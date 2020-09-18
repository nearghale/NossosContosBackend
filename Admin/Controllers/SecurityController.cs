using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sugar.Admin.Models;

namespace Sugar.Admin.Controllers
{
    [Authorize]
    public class SecurityController : Controller
    {

        private Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Admin> administratorRepository;

        public SecurityController(Nossos_Contos.Models.MongoDB.DatabaseSettings databaseSettings)
        {
            administratorRepository = new Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Admin>(databaseSettings, "admin");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.Administrator = HttpContext.User.Identity.Name;
            }
            base.OnActionExecuting(context);
        }

    }
}