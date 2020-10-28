﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Auth
{
    public class ThrottledHttpClient
    {
        private HttpClient client;
        public ThrottledHttpClient(HttpClient client)
        {
            this.client = client;
        }

        private static SemaphoreSlim semaphore = new SemaphoreSlim(10, 10);
        public async Task<HttpResponseMessage> PostAsync(HttpRequestMessage message)
        {
            await semaphore.WaitAsync();
            var task = client.SendAsync(message);
            await Task.Delay(TimeSpan.FromSeconds(5));
            semaphore.Release();
            return await task;
        }
    }
}