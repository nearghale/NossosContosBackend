using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Models.MongoDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]
   //[Authorize]

    public class MeController : Base.BaseController
    {

  

        public MeController(DatabaseSettings databaseSettings, Models.Configurations.AWS.S3Configuration s3Configuration,
                            Models.Configurations.AWS.Credentials credentials) : base(databaseSettings, s3Configuration, credentials)
        {

        }


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


            return mediaService.Create(file.OpenReadStream(), type, extension);

        }


        [HttpPut("{id}")]
        public ActionResult Update(string id, Models.AccountUpdate accountModel)
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
