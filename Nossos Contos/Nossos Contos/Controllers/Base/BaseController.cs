using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Helpers;
using Nossos_Contos.Models;
using Nossos_Contos.Services;
using Nossos_Contos.Models.MongoDB;
using Nossos_Contos.Models.Configurations.AWS;
using Microsoft.IdentityModel.JsonWebTokens;
using Nossos_Contos.Models.AWS.Cognito;

namespace Nossos_Contos.Controllers.Base
{
    [ApiController]
    public abstract class BaseController : Controller
    {

        protected Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Complaint> complaintRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Comment> commentRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Media> mediaRepository;
		protected Repositories.MongoDB.PersistentRepository<Entities.Admin> adminRepository;

		protected MediaService mediaService;
        protected AdminService adminService;
        protected AccountService accountService;
        protected TaleService taleService;
        protected CommentService commentService;
        protected ComplaintService complaintService;

        protected DeleteNextDependenciesHelper deleteNextDependencies;


        public BaseController(DatabaseSettings databaseSettings)
        {
            //repositorys
            accountRepository = new Repositories.MongoDB.PersistentRepository<Entities.Account>(databaseSettings, "account");
            generalInformationRepository = new Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation>(databaseSettings, "general-information");
            taleRepository = new Repositories.MongoDB.PersistentRepository<Entities.Tale>(databaseSettings, "tale");
            commentRepository = new Repositories.MongoDB.PersistentRepository<Entities.Comment>(databaseSettings, "comment");
            complaintRepository = new Repositories.MongoDB.PersistentRepository<Entities.Complaint>(databaseSettings, "complaint");
			adminRepository = new Repositories.MongoDB.PersistentRepository<Entities.Admin>(databaseSettings, "admin");




			//services
			accountService = new AccountService(accountRepository, generalInformationRepository);
            adminService = new AdminService(adminRepository, complaintRepository, taleRepository, accountRepository, generalInformationRepository);
            taleService = new TaleService(taleRepository, generalInformationRepository);
            commentService = new CommentService(taleRepository, commentRepository);
            complaintService = new ComplaintService(taleRepository, complaintRepository);
		

			//helpers
			deleteNextDependencies = new DeleteNextDependenciesHelper(taleRepository, commentRepository, generalInformationRepository, mediaRepository);


        }

		public BaseController(DatabaseSettings databaseSettings, S3Configuration s3Configuration, Credentials credentials)
		{
			accountRepository = new Repositories.MongoDB.PersistentRepository<Entities.Account>(databaseSettings, "account");
			mediaRepository = new Repositories.MongoDB.PersistentRepository<Entities.Media>(databaseSettings, "media");
			generalInformationRepository = new Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation>(databaseSettings, "general-information");
			commentRepository = new Repositories.MongoDB.PersistentRepository<Entities.Comment>(databaseSettings, "comment");
			taleRepository = new Repositories.MongoDB.PersistentRepository<Entities.Tale>(databaseSettings, "tale");
			adminRepository = new Repositories.MongoDB.PersistentRepository<Entities.Admin>(databaseSettings, "admin");



			mediaService = new MediaService(mediaRepository, credentials, s3Configuration);
			accountService = new AccountService(accountRepository, generalInformationRepository);
			adminService = new AdminService(adminRepository, complaintRepository, taleRepository, accountRepository, generalInformationRepository);



			deleteNextDependencies = new DeleteNextDependenciesHelper(taleRepository, commentRepository, generalInformationRepository, mediaRepository);

		}

    

		//-------COGNITO-------//

		private string bearerToken;
		public string BearerToken
		{
			get
			{
				if (String.IsNullOrEmpty(bearerToken))
				{
					bearerToken = Request.Headers["Authorization"][0].Replace("Bearer ", "");
				}
				return bearerToken;
			}
		}

		private CognitoUser cognitoUser;
		public CognitoUser CognitoUser
		{
			get
			{
				if (cognitoUser == null)
				{
					JsonWebToken jwt = new JsonWebToken(BearerToken);
					cognitoUser = new CognitoUser();
					foreach (var claim in jwt.Claims)
					{
						switch (claim.Type)
						{
							case "sub":
								var sub = Guid.Empty;
								if (Guid.TryParse(claim.Value, out sub))
									cognitoUser.sub = sub;
								break;
							case "family_number":
								cognitoUser.family_name = claim.Value;
								break;
							case "name":
								cognitoUser.name = claim.Value;
								break;
							case "user_name":
								cognitoUser.user_name = claim.Value;
								break;
							case "email":
								cognitoUser.email = claim.Value;
								break;
							case "birth_date":
								cognitoUser.birth_date = claim.Value;
								break;
							case "password":
								cognitoUser.password = claim.Value;
								break;
						}
					}
				}
				return cognitoUser;
			}
		}


		public IActionResult Process(ApiResponse result)
		{
			if (result.error)
			{
				switch (result.code)
				{
					case (int)ApiResponse.Codes.ITEM_NOT_FOUND:
						return NotFound(result);
					case (int)ApiResponse.Codes.ITEM_ALREADY_EXISTS:
					case (int)ApiResponse.Codes.INVALID_INPUT:
						return Conflict(result);
					case (int)ApiResponse.Codes.RESOURCE_UNAVAILABLE:
					case (int)ApiResponse.Codes.UNABLE_PROCESS_REQUEST:
						return Unauthorized(result);
					default:
						return BadRequest(result);
				}
			}
			return Ok(result);
		}



	}
}