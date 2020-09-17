using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Services;
using Nossos_Contos.Models;
using Nossos_Contos.Helpers;
using Nossos_Contos.Models.MongoDB;
using Microsoft.AspNetCore.Authorization;
using Nossos_Contos.Models.AWS.Cognito;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class AccountController : Base.BaseController
    {
     
        public AccountController(DatabaseSettings databaseSettings) : base(databaseSettings)
        {

        }

        [HttpPost]
        public ActionResult<Entities.Account> Create(Entities.Account account)
        {

            var newAccount = accountRepository.FirstOrDefault(a => a.UserId == this.CognitoUser.sub) ;
            if (newAccount != null)
               return Unauthorized("USER_ALREADY_EXISTS");

            return accountService.Create(this.CognitoUser, account);

        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public ActionResult SingUp(SignUp model)
        {

            var cognitoService = new Services.AWS.CognitoService();
            cognitoService.SignUp(model);
            return Ok();
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<Token> Authenticate(Authentication model)
        {
            var cognitoService = new Services.AWS.CognitoService();
            return cognitoService.Authenticate(model);
        }



    }
}