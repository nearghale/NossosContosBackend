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

    public class ComplaintController : Controller
    {

        private Repositories.MongoDB.PersistentRepository<Entities.Complaint> complaintRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository;
        private ComplaintService complaintService;

        public ComplaintController(DatabaseSettings databaseSettings)
        {

            complaintRepository = new Repositories.MongoDB.PersistentRepository<Entities.Complaint>(databaseSettings, "complaint");
            taleRepository = new Repositories.MongoDB.PersistentRepository<Entities.Tale>(databaseSettings, "tale");
            complaintService = new ComplaintService(taleRepository, complaintRepository);

        }

        [HttpPost]
        public ActionResult<Entities.Complaint> Create(Entities.Complaint complaint)
        {
            var tale = taleRepository.FirstOrDefault(t => t.id == complaint.IDTale);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            return complaintService.Create(complaint);
            
        }

    }
}
