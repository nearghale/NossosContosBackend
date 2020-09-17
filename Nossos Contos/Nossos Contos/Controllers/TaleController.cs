using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Models;
using Nossos_Contos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nossos_Contos.Helpers;
using Nossos_Contos.Models.MongoDB;
using Microsoft.AspNetCore.Authorization;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class TaleController : Base.BaseController
    {


        //TODO: Resolver construtor
        public TaleController(DatabaseSettings databaseSettings) : base(databaseSettings)
        {

        }

        [HttpPost]
        public ActionResult<Entities.Tale> Create(Entities.Tale tale)
        {

            var account = accountRepository.FirstOrDefault(a => a.UserId == this.CognitoUser.sub);
            if (account == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            return taleService.Create(account, tale);

        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, TaleUpdate taleUpdate)
        {
            var tale = taleRepository.FirstOrDefault(t => t.IDTale == id);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            taleService.Update(tale, taleUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var tale = taleRepository.FirstOrDefault(t => t.IDTale == id);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            deleteNextDependencies.DeleteTaleDependencies(id);

            taleService.Delete(tale);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<List<Entities.Tale>> GetTalesAuthor(Guid id)
        {
            var account = accountRepository.FirstOrDefault(a => a.UserId == id);
            if (account == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            return taleService.GetTalesAuthor(id);

        }

    }
}
