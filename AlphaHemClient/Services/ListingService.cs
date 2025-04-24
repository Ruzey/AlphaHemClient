using AlphaHemAPI.Data.DTO;
using System.Net.Http.Json;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    public class ListingService
    {
        private readonly HttpClient _http;

        public ListingService(HttpClient http)
        {
            _http = http;
        }
    }
}
