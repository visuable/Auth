using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        private IHttpClientFactory _clientFactory;
        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public void Index()
        {

        }
        [HttpGet]
        public async void SendRequest()
        {
            var client = _clientFactory.CreateClient("ContextClient");
            var thClient = new ThrottledHttpClient(client);
            for (var i = 0; i < 100; i++)
            {
               await thClient.PostAsync(new HttpRequestMessage()
               {
                 RequestUri = new System.Uri("https://yandex.ru")
               });
            }
            return;
        }
    }
}
