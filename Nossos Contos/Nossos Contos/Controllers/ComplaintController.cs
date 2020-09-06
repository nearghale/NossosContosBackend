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

    public class ComplaintController : Base.BaseController
    {

     

        public ComplaintController(DatabaseSettings databaseSettings) : base(databaseSettings)
        {

        }

        [HttpPost]
        public ActionResult<Entities.Complaint> Create(Entities.Complaint complaint)
        {
            var tale = taleRepository.FirstOrDefault(t => t.id == complaint.IDTale);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            return complaintService.Create(tale, complaint);
            
        }

    }
}
