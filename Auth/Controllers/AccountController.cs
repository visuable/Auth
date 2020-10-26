using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                contextUser.Password = user.Password;
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
            if (context.Users.Find(user.Login) == null)
            {
                return RedirectToAction("Register");
            }
            string encoded = GenerateToken();
            return Json(encoded);
        }

        private string GenerateToken()
        {
            var token = new JwtSecurityToken(
                            issuer: Settings.Issuer,
                            audience: Settings.Audience,
                            notBefore: DateTime.Now,
                            expires: DateTime.Now.Add(TimeSpan.FromMinutes(Settings.Lifetime)),
                            signingCredentials: new SigningCredentials(Settings.GetSymmetricKey(), SecurityAlgorithms.HmacSha256)
                            );
            var encoded = new JwtSecurityTokenHandler().WriteToken(token);
            return encoded;
        }
    }
}
