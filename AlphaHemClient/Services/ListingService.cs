using AlphaHemClient.Model.DTO;
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

        public async Task<PagedListingListDto> GetPaginatedListings(int pageIndex = 1, int pageSize = 10, string? municipality = null, string? sortBy = null)
        {
            try
            {
                List<string> queryParams = new List<String>();

                queryParams.Add($"pageIndex={pageIndex}");
                queryParams.Add($"pageSize={pageSize}");

                if (!string.IsNullOrEmpty(municipality))
                {
                    queryParams.Add($"municipality={municipality}");
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

                var response = await _http.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var listings = await response.Content.ReadFromJsonAsync<PagedListingListDto>();
                    return listings ?? new PagedListingListDto();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Tekniskt fel: {error}");
                    throw new Exception("Kunde inte hämta bostadsdata.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching listings: {ex.Message}");
                return new PagedListingListDto();
            }
        }
    }
}
