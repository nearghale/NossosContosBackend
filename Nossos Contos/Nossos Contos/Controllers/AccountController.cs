using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Services;
using Nossos_Contos.Model;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class AccountController : Controller
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository;
        private AccountService accountService;

        // testando nova branch do rafael

        public AccountController(DatabaseSettings databaseSettings)
        {
        
            accountRepository = new Repositories.MongoDB.PersistentRepository<Entities.Account>(databaseSettings, "account");
            accountService = new AccountService(accountRepository);
        
        }

        [HttpPost]
        public ActionResult<Entities.Account> Create(Entities.Account account)
        {

            var newAccount = accountRepository.FirstOrDefault(a => a.UserName == account.UserName);
            if (newAccount != null)
               return Unauthorized("USER_ALREADY_EXISTS");

            return accountService.Create(account);

        }

        [HttpGet]
        public ActionResult<List<Entities.Account>> GetAllAccounts()
        {
            return accountService.GetAllAccounts();

        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, Model.AccountUpdate accountModel)
        {

            var accountUser = accountRepository.FirstOrDefault(a => a.id == id);
            if (accountUser == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            accountService.Update(accountUser, accountModel);
            var userUpdate = accountService.GetAccount(id);
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var user = accountRepository.FirstOrDefault(a => a.id == id);
            if (user == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            accountService.Delete(id);
            return Ok();
        }



    }
}