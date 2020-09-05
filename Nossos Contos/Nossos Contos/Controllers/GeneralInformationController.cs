using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Entities;
using Nossos_Contos.Model;

namespace Nossos_Contos.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class GeneralInformationController : Controller
    {
        private Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation> generalInformationRepository;

        public GeneralInformationController(DatabaseSettings databaseSettings)
        {
            generalInformationRepository = new Repositories.MongoDB.PersistentRepository<Entities.GeneralInformation>(databaseSettings, "general-information");

        }

        [HttpPost]
        public Entities.GeneralInformation Create(Entities.GeneralInformation generalInformation)
        {
            return generalInformationRepository.Create(generalInformation);
        }


        [HttpGet]
        public List<Entities.GeneralInformation> GetGeneralInformation()
        {
            return generalInformationRepository.Find(g => true);
        }


    }
}