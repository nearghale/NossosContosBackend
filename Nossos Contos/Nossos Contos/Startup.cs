using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System;


namespace Nossos_Contos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {

            // requires using Microsoft.Extensions.Options
            services.Configure<Models.MongoDB.DatabaseSettings>(
                Configuration.GetSection(nameof(Models.MongoDB.DatabaseSettings)));

            services.Configure<Models.MongoDB.DatabaseSettings>(Configuration.GetSection("AccountDatabaseSettings"));
            services.Configure<Models.Configurations.AWS.Credentials>(Configuration.GetSection("AWSCredentials"));
            services.Configure<Models.Configurations.AWS.S3Configuration>(Configuration.GetSection("AWSS3"));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Models.MongoDB.DatabaseSettings>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Models.Configurations.AWS.Credentials>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Models.Configurations.AWS.S3Configuration>>().Value);


            
            var cognitoConfiguration = Configuration.GetSection("AWSCognito").Get<Nossos_Contos.Models.Configurations.AWS.CognitoConfiguration>();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                        {
                            var json = new WebClient().DownloadString(parameters.ValidIssuer + "/.well-known/jwks.json");
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
                        },
                        ValidateIssuer = true,
                        ValidIssuer = $"https://cognito-idp.{cognitoConfiguration.Region}.amazonaws.com/{cognitoConfiguration.PoolId}",
                        ValidateLifetime = true,
                        LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
                        ValidateAudience = true,
                        ValidAudience = cognitoConfiguration.AppClientId,
                    };
                });





            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
