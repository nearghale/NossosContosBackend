using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sugar.Admin.Controllers
{
    public class AdministratorsController : Controller
    {
        private Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Admin> adminRepository;

        public AdministratorsController(Nossos_Contos.Models.MongoDB.DatabaseSettings databaseSettings) 
        {
            adminRepository = new Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Admin>(databaseSettings, "admin");
        }

        public IActionResult Index()
        {
            var admins = adminRepository.Find(a => true);
            return View(admins);
        }
    }
}