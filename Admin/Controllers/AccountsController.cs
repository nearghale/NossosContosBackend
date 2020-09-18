using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sugar.Admin.Controllers
{
    public class AccountsController : Controller
    {
        private Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Account> accountRepository;

        public AccountsController(Nossos_Contos.Models.MongoDB.DatabaseSettings databaseSettings) 
        {
            accountRepository = new Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Account>(databaseSettings, "account");
        }

        public IActionResult Index()
        {
            var accounts = accountRepository.Find(a => true);
            return View(accounts);
        }

        public virtual IActionResult Delete(Guid id)
        {
            accountRepository.Remove(a => a.UserId == id);
            return RedirectToAction("index");
        }

        public virtual IActionResult Details(Guid id)
        {
            var account = accountRepository.FirstOrDefault(t => t.UserId == id);
            if (account != null)
                return View(account);

            return RedirectToAction("index");
        }


    }
}