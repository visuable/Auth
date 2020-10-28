using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public void Index()
        {

        }
        [HttpGet]
        public async Task<IActionResult> SendRequest()
        {
            //var client = _clientFactory.CreateClient("ContextClient");
            var resp = new List<HttpResponseMessage>();
            for (var i = 0; i < 5; i++)
            {
               resp.Add(await client.PostAsync(new HttpRequestMessage()
               {
                 RequestUri = new System.Uri("https://yandex.ru")
               }));
            }
            return Json(resp);
        }
    }
}
