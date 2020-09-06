using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Helpers;
using Nossos_Contos.Model;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class AdminController : Controller
    {

        private Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Complaint> complaintRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Comment> commentRepository;


        private Services.AdminService adminService;
        private Services.AccountService accountService;
        private Services.TaleService taleService;

        private DeleteNextDependenciesHelper deleteNextDependencies;



        public AdminController(DatabaseSettings databaseSettings)
        {

            complaintRepository = new Repositories.MongoDB.PersistentRepository<Entities.Complaint>(databaseSettings, "complaint");
            taleRepository = new Repositories.MongoDB.PersistentRepository<Entities.Tale>(databaseSettings, "tale");
            accountRepository = new Repositories.MongoDB.PersistentRepository<Entities.Account>(databaseSettings, "account");
            commentRepository = new Repositories.MongoDB.PersistentRepository<Entities.Comment>(databaseSettings, "comment");
            generalInformationRepository = new Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation>(databaseSettings, "general-information");


            adminService = new Services.AdminService(complaintRepository, taleRepository, accountRepository, generalInformationRepository);
            accountService = new Services.AccountService(accountRepository, generalInformationRepository);
            taleService = new Services.TaleService(taleRepository, generalInformationRepository);

            deleteNextDependencies = new DeleteNextDependenciesHelper(taleRepository, commentRepository, generalInformationRepository);

        }

        [HttpGet("primeiro-dia")]
        public DateTime primeiroDiaDoMês()
        {
            //Vamos considerar que a data seja o dia de hoje, mas pode ser qualquer data.
            DateTime data = DateTime.Today;

            //DateTime com o primeiro dia do mês
            DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);
            return primeiroDiaDoMes;
        }


        [HttpGet("reported-tales")]
        public ActionResult<List<Entities.Tale>> GetReportedTales()
        {
            return adminService.GetReportedTales();
        }

        [HttpGet("accounts")]
        public ActionResult<List<Entities.Account>> GetAllAccounts()
        {
            return adminService.GetAllAccounts();

        }

        [HttpGet("total-tales")]
        public long GetTotalTales()
        {
            return adminService.GetTotalTales();
        }

        [HttpGet("total-tales-month")]
        public long GetTotalTalesMonth()
        {
            return adminService.GetTotalTalesMonth();
        }


        [HttpDelete("delete-tale/{id}")]
        public ActionResult DeleteTale(string id)
        {
            var tale = taleRepository.FirstOrDefault(t => t.id == id);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            deleteNextDependencies.DeleteTaleDependencies(id);

            taleService.Delete(tale);
            return Ok();
        }

        [HttpDelete("delete-account/{id}")]
        public ActionResult DeleteAccount(string id)
        {
            var user = accountRepository.FirstOrDefault(a => a.id == id);
            if (user == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            deleteNextDependencies.DeleteAccountDependencies(id);

            accountService.Delete(id);
            return Ok();
        }



      
    }
}