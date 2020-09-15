﻿using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Models;
using Nossos_Contos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nossos_Contos.Helpers;
using Nossos_Contos.Models.MongoDB;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class TaleController : Base.BaseController
    {



        public TaleController(DatabaseSettings databaseSettings) : base(databaseSettings)
        {

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
        public ActionResult Update(string id, Models.TaleUpdate taleUpdate)
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

            deleteNextDependencies.DeleteTaleDependencies(id);

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
