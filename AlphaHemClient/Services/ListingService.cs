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

        public async Task<List<ListingListDto>> GetAllListings(int? municipalityId = null, string sortBy = null)
        {
            try
            {
                List<string> queryParams = new List<String>();

                if (municipalityId.HasValue)
                {
                    queryParams.Add($"municipalityId={municipalityId.Value}");
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    queryParams.Add($"sortBy={sortBy}");
                }

                string url = "api/Listing";
                if (queryParams.Any())
                {
                    url += "?" + string.Join("&", queryParams);
                }

                var listings = await _http.GetFromJsonAsync<List<ListingListDto>>(url);

                return listings ?? new List<ListingListDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching listings: {ex.Message}");
                return new List<ListingListDto>();
            }
        }
    }
}
