using System.Net.Http.Headers;

namespace AlphaHemClient.Services
{
    //Author: ALL
    public class BaseHttpService
    {
        private readonly HttpClient client;
        protected readonly AuthService authService;

        public BaseHttpService(HttpClient client, AuthService authService)
        {
            this.client = client;
            this.authService = authService;
        }

        protected async Task GetBearerToken()
        {
            var token = await authService.GetAccessTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
