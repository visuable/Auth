using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        private ThrottledHttpClient client;
        private IHttpClientFactory factory;
        public HomeController(ThrottledHttpClient client, IHttpClientFactory factory)
        {
            this.client = client;
            this.factory = factory;
        }
        public void Index()
        {

        }
        [HttpGet]
        public async Task<IActionResult> SendRequest()
        {
            var list = new List<JsonResult>();
            for (var i = 0; i < 15; i++)
            {
                var r = await client.PostAsync(new HttpRequestMessage()
                {
                    RequestUri = new System.Uri("https://api.vk.com/method/users.get?access_token=467a0747acb862daa4648c654a67cdfbfb40d087da2be277c7bdaeda2678d31588a20027cd0253d74df33&v=5.124")
                });
                list.Add(Json(r.Content.ReadAsStringAsync()));
            }
            return Json(list);
        }
        [HttpGet]
        public async Task<IActionResult> SendBadRequest()
        {
            var client = factory.CreateClient("ContextClient");
            var list = new List<JsonResult>();
            for (var i = 0; i < 15; i++)
            {
                var data = new StringContent("");
                var r = await client.PostAsync("https://api.vk.com/method/users.get?access_token=467a0747acb862daa4648c654a67cdfbfb40d087da2be277c7bdaeda2678d31588a20027cd0253d74df33&v=5.124", data);
                list.Add(Json(await r.Content.ReadAsStringAsync()));
            }
            return Json(list);
        }
    }
}
