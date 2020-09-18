using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sugar.Admin.Models;

namespace Sugar.Admin.Controllers
{
    public class HomeController : SecurityController
    {

        private Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.GeneralInformation> generalInformationRepository;


        public HomeController(Nossos_Contos.Models.MongoDB.DatabaseSettings databaseSettings) : base(databaseSettings)
        {
            generalInformationRepository = new Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.GeneralInformation>(databaseSettings, "general-information");

        }

        public IActionResult Index()
        {
            var generalInformation = generalInformationRepository.FirstOrDefault(g => true);
            if (generalInformation != null)
                return View(generalInformation);

            return View(generalInformation);
        }

    }
}