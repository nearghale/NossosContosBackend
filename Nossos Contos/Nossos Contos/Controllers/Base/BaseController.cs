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


namespace Nossos_Contos.Controllers.Base
{
    public class BaseController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Complaint> complaintRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Comment> commentRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Media> mediaRepository;
    
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

    
                        

            //services
            accountService = new AccountService(accountRepository, generalInformationRepository);
            adminService = new AdminService(complaintRepository, taleRepository, accountRepository, generalInformationRepository);
            taleService = new TaleService(taleRepository, generalInformationRepository);
            commentService = new CommentService(taleRepository, commentRepository);
            complaintService = new ComplaintService(taleRepository, complaintRepository);

            //helpers
            deleteNextDependencies = new DeleteNextDependenciesHelper(taleRepository, commentRepository, generalInformationRepository);

        }

        public BaseController(DatabaseSettings databaseSettings, S3Configuration s3Configuration, Credentials credentials)
        {

            mediaRepository = new Repositories.MongoDB.PersistentRepository<Entities.Media>(databaseSettings, "media");
            mediaService = new MediaService(mediaRepository, credentials, s3Configuration);

        }



    }
}