using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Auth
{
    public class ThrottledHttpClient
    {
        private IHttpClientFactory factory;
        private IConfiguration _configuration;
        public ThrottledHttpClient(IHttpClientFactory factory, IConfiguration configuration)
        {
            this.factory = factory;
            _configuration = configuration;
            var sett = int.Parse(_configuration["ThrottledClientSettings:RequeriesLimit"]);
            semaphore = new SemaphoreSlim(sett, sett);
            client = this.factory.CreateClient("Client");
            delay = int.Parse(_configuration["ThrottledClientSettings:Delay"]);
        }

        private static SemaphoreSlim semaphore;
        private static HttpClient client;
        private static int delay;
        public async Task<HttpResponseMessage> PostAsync(HttpRequestMessage message)
        {
            await semaphore.WaitAsync();
            var task = client.SendAsync(message);
            await Task.Delay(TimeSpan.FromSeconds(delay));
            semaphore.Release();
            return await task;
        }
    }
}
