using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        private ThrottledHttpClient client;
        public HomeController(ThrottledHttpClient client)
        {
            this.client = client;
        }
        [HttpGet]
        public async Task SendRequest()
        {
            for (var i = 0; i < 100; i++)
            {
                await client.PostAsync(new HttpRequestMessage()
                {
                    RequestUri = new System.Uri("https://yandex.ru")
                });
            }
            return;
        }
    }
}
