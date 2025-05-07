using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace AlphaHemClient.Services
{
    //Author: ALL
    public class BaseHttpService
    {
        private readonly HttpClient client;
        private readonly ILocalStorageService localStorage;

        public BaseHttpService(HttpClient client, ILocalStorageService localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;
        }

        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
