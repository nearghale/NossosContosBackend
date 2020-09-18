using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Helpers;
using Nossos_Contos.Models;
using Nossos_Contos.Models.MongoDB;


namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class AdminController : Base.BaseController
    {

        public AdminController(DatabaseSettings databaseSettings, Models.Configurations.AWS.S3Configuration s3Configuration,
                            Models.Configurations.AWS.Credentials credentials) : base(databaseSettings, s3Configuration, credentials)
        {

        }


        [HttpPost]
        public ActionResult<Entities.Admin> Create(Entities.Admin newAdmin)
        {
            var admin = adminRepository.FirstOrDefault(a => a.Email == newAdmin.Email);
            if(admin != null)
                return Unauthorized("USER_ALREADY_EXISTS");

           return adminService.Create(newAdmin);
        }



        [HttpGet("first-day")]
        public DateTime PrimeiroDiaDoMês()
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
        public ActionResult DeleteTale(Guid id)
        {
            var tale = taleRepository.FirstOrDefault(t => t.IDTale == id);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            deleteNextDependencies.DeleteTaleDependencies(id);

            taleService.Delete(tale);
            return Ok();
        }

        [HttpDelete("delete-account/{id}")]
        public ActionResult DeleteAccount(Guid id)
        {
            var user = accountRepository.FirstOrDefault(a => a.UserId == id);
            if (user == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            deleteNextDependencies.DeleteAccountDependencies(id);

            accountService.Delete(user);
            return Ok();
        }



      
    }
}