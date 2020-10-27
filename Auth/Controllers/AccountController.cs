using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Controllers
{
    public class AccountController : Controller
    {
        private IAccountManager _manager;
        public AccountController(IAccountManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(ViewUser user)
        {
            _manager.RegisterUser(user);
            return Content("Успешная регистрация");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(ViewUser user)
        {
            return Json(_manager.LoginUser(user));
        }
    }
}
