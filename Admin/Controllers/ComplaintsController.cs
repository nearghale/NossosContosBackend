using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sugar.Admin.Controllers
{
    public class ComplaintsController : Controller
    {
        private Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Complaint> complaintRepository;

        public ComplaintsController(Nossos_Contos.Models.MongoDB.DatabaseSettings databaseSettings) 
        {
            complaintRepository = new Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Complaint>(databaseSettings, "complaint");
        }

        public IActionResult Index()
        {
            var accounts = complaintRepository.Find(a => true);
            return View(accounts);
        }

        public virtual IActionResult Delete(Guid id)
        {
            complaintRepository.Remove(r => r.IDComplaint == id);
            return RedirectToAction("index");
        }

        public virtual IActionResult Details(Guid id)
        {
            var item = complaintRepository.FirstOrDefault(c => c.IDComplaint == id);
            if (item != null)
                return View(item);

            return RedirectToAction("index");
        }

    }
}