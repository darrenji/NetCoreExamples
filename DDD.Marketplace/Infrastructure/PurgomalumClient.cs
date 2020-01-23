using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DDD.Marketplace.Infrastructure
{
    public class PurgomalumClient
    {
        private readonly HttpClient _httpClient;

        public PurgomalumClient() : this(new HttpClient()) { }

        public PurgomalumClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> CheckForProfanity(string text)
        {
            var result = await _httpClient.GetAsync(
                QueryHelpers.AddQueryString("https://www.purgomalum.com/service/containsprofanity", "text", text));

            var value = await result.Content.ReadAsStringAsync();
            return bool.Parse(value);
        }
    }
}
