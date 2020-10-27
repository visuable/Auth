using Auth.Models;
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
        private AppContext context;
        public AccountController(AppContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(ViewUser user)
        {
            var contextUser = new User();
            if (context.Users.Find(user.Login) == null)
            {
                contextUser.Role = "user";
                contextUser.Login = user.Login;
                contextUser.Salt = Encrypts.GenerateSalt();
                contextUser.Password = Encrypts.EncryptPassword(user.Password, contextUser.Salt);
                context.Users.Add(contextUser);
            }
            context.SaveChanges();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(ViewUser user)
        {
            var dbUser = context.Users.Find(user.Login);
            if (dbUser == null)
            {
                return RedirectToAction("Register");
            }
            if (dbUser.Password == Encrypts.EncryptPassword(user.Password, dbUser.Salt))
            {
                string encoded = Encrypts.GenerateToken();
                return Json(encoded);
            }
            return Content("Неправильный пароль!");
        }
    }
}
