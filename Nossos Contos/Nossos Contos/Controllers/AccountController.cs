using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Services;
using Nossos_Contos.Model;
using Nossos_Contos.Helpers;
using Nossos_Contos.Model.MongoDB;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class AccountController : Base.BaseController
    {
     
        //testando commit
        public AccountController(DatabaseSettings databaseSettings) : base(databaseSettings)
        {

        }

        [HttpPost]
        public ActionResult<Entities.Account> Create(Entities.Account account)
        {

            var newAccount = accountRepository.FirstOrDefault(a => a.UserName == account.UserName);
            if (newAccount != null)
               return Unauthorized("USER_ALREADY_EXISTS");

            return accountService.Create(account);

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

            deleteNextDependencies.DeleteAccountDependencies(id);

            accountService.Delete(id);
            return Ok();
        }



    }
}