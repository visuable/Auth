using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        [JWTAuthFilter]
        public IActionResult Secret()
        {
            return Content("Авторизованный пользователь.");
        }
    }
}
