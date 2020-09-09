using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nossos_Contos.Model;
using Nossos_Contos.Services;

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
        public void ConfigureServices(IServiceCollection services)
        {
         
            // requires using Microsoft.Extensions.Options
            services.Configure<Model.MongoDB.DatabaseSettings>(
                Configuration.GetSection(nameof(Model.MongoDB.DatabaseSettings)));

            services.Configure<Model.MongoDB.DatabaseSettings>(Configuration.GetSection("AccountDatabaseSettings"));
            services.Configure<Model.Configurations.AWS.Credentials>(Configuration.GetSection("AWSCredentials"));
            services.Configure<Model.Configurations.AWS.S3Configuration>(Configuration.GetSection("AWSS3"));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Model.MongoDB.DatabaseSettings>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Model.Configurations.AWS.Credentials>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Model.Configurations.AWS.S3Configuration>>().Value);



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
