using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private Services.AdminService adminService;

        public AdminController(DatabaseSettings databaseSettings)
        {

            complaintRepository = new Repositories.MongoDB.PersistentRepository<Entities.Complaint>(databaseSettings, "complaint");
            taleRepository = new Repositories.MongoDB.PersistentRepository<Entities.Tale>(databaseSettings, "tale");
            accountRepository = new Repositories.MongoDB.PersistentRepository<Entities.Account>(databaseSettings, "account");

            adminService = new Services.AdminService(complaintRepository, taleRepository, accountRepository);


        }

        [HttpGet("reported-tales")]
        public ActionResult<List<Entities.Tale>> GetReportedTales()
        {
            return adminService.GetReportedTales();
        }

        [HttpDelete("delete-tale/{id}")]
        public ActionResult DeleteTale(string id)
        {
            var tale = taleRepository.FirstOrDefault(t => t.id == id);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            adminService.DeleteTale(tale);
            return Ok();
        }

        [HttpDelete("delete-account/{id}")]
        public ActionResult DeleteAccount(string id)
        {
            var user = accountRepository.FirstOrDefault(a => a.id == id);
            if (user == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            adminService.DeleteAccount(id);
            return Ok();
        }

      
    }
}