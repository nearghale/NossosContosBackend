using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class AccountController : Controller
    {

        [HttpGet]
        public string Index()
        {
            return "olá mundo!";
        }
    }
}