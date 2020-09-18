using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Sugar.Admin.Models;
using System.Security.Claims;

namespace Sugar.Admin.Controllers
{
    public class LoginController : Controller
    {

        private Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Admin> administratorRepository;

        public LoginController(Nossos_Contos.Models.MongoDB.DatabaseSettings databaseSettings)
        {
            administratorRepository = new Nossos_Contos.Repositories.MongoDB.PersistentRepository<Nossos_Contos.Entities.Admin>(databaseSettings, "admin");
        }

        public IActionResult Index(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password, string returnUrl = null)
        {
            var administrator = administratorRepository.FirstOrDefault(a => a.Email == email && a.Password == password);
            if(administrator != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("user", administrator.Name),
                    new Claim("role", "Member")
                };
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("index", "home");
                }
            }
            return RedirectToAction("index", new { returnUrl });
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index");
        }

    }
}