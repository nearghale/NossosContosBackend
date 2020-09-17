using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Models.MongoDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nossos_Contos.Models.AWS.Cognito;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class MeController : Base.BaseController
    {



        public MeController(DatabaseSettings databaseSettings, Models.Configurations.AWS.S3Configuration s3Configuration,
                            Models.Configurations.AWS.Credentials credentials) : base(databaseSettings, s3Configuration, credentials)
        {

        }

        //TODO: associar com um usuário
        [HttpPost("post-media")]
        public ActionResult<Entities.Media> SendFile(IFormFile file)
        {

            var info = new FileInfo(file.FileName);
            var extension = info.Extension.ToLower().Replace(".", "");
            var type = mediaService.GetTypeByExtension(extension);
            if (type == Entities.Media.Types.Unknown)
            {
                return BadRequest("INVALID_EXTENSIONS");
            }


            return mediaService.Create(file.OpenReadStream(), type, extension, CognitoUser.sub);

        }



        [HttpPut]
        public ActionResult Update(Models.AccountUpdate accountModel)
        {

            var accountUser = accountRepository.FirstOrDefault(a => a.UserId == this.CognitoUser.sub);
            if (accountUser == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            accountService.Update(accountUser, accountModel);
            return Ok();

        }

        [HttpDelete]
        public ActionResult Delete()
        {
            var user = accountRepository.FirstOrDefault(a => a.UserId == this.CognitoUser.sub);
            if (user == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            deleteNextDependencies.DeleteAccountDependencies(user.UserId);

            accountService.Delete(user);
            return Ok();
        }

        [HttpDelete("delete-cognito-user/{username}")]
        public ActionResult DeleteCognito(string username)
        {

            var cognitoService = new Services.AWS.CognitoService();
            cognitoService.Delete(username);
            return Ok();
        }

        [HttpPut("update-cognito-user/{username}")]
        public ActionResult UpdateCognito(string username, UpdateUserCognito updateUserCognito)
        {

            var cognitoService = new Services.AWS.CognitoService();
            cognitoService.Update(username, updateUserCognito);
            return Ok();
        }




    }
}
