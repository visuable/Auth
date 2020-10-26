using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Secret()
        {
            return Content("Авторизованный пользователь.");
        }
    }
}
