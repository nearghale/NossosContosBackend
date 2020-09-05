using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Model;
using Nossos_Contos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class TaleController : Controller
    {

        private Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Account> accountRepository;
        private TaleService taleService;

        public TaleController(DatabaseSettings databaseSettings)
        {

            taleRepository = new Repositories.MongoDB.PersistentRepository<Entities.Tale>(databaseSettings, "tale");
            accountRepository = new Repositories.MongoDB.PersistentRepository<Entities.Account>(databaseSettings, "account");
            taleService = new TaleService(accountRepository, taleRepository);

        }

        [HttpPost]
        public ActionResult<Entities.Tale> Create(Entities.Tale tale)
        {

            //testando branch nova
            var account = accountRepository.FirstOrDefault(a => a.id == tale.IDUser);
            if (account == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            return taleService.Create(account, tale);

        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, Model.TaleUpdate taleUpdate)
        {
            var tale = taleRepository.FirstOrDefault(t => t.id == id);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            taleService.Update(tale, taleUpdate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var tale = taleRepository.FirstOrDefault(t => t.id == id);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            taleService.Delete(tale);
            return Ok();
        }


        [HttpGet("{id}")]
        public ActionResult<List<Entities.Tale>> GetTalesAuthor(string id)
        {
            var account = accountRepository.FirstOrDefault(a => a.id == id);
            if (account == null)
                return this.Unauthorized("USER_NOT_FOUNDED");

            return taleService.GetTalesAuthor(id);

        }

    }
}
