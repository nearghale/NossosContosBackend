using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sugar.Admin.Controllers
{
    public class TalesController : Controller
    {
        private Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Tale> taleRepository;

        public TalesController(Nossos_Contos.Models.MongoDB.DatabaseSettings databaseSettings) 
        {
            taleRepository = new Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Tale>(databaseSettings, "tale");
        }

        public IActionResult Index()
        {
            var tales = taleRepository.Find(a => true);
            return View(tales);
        }

        public virtual IActionResult Delete(Guid id)
        {
            taleRepository.Remove(r => r.IDTale == id);
            return RedirectToAction("index");
        }


        public virtual IActionResult Details(Guid id)
        {
            var item = taleRepository.FirstOrDefault(t => t.IDTale == id);
            if (item != null)
                return View(item);

            return RedirectToAction("index");
        }
    }
}