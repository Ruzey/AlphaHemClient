using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
using System.Net.Http.Json;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    public class ListingService
    {
        private readonly HttpClient _http;
        private readonly IMapper _mapper;

        public ListingService(HttpClient http, IMapper mapper)
        {
            _http = http;
            _mapper = mapper;
        }

        public async Task<ListingPageViewModel> GetPaginatedListings(int pageIndex = 1, int pageSize = 10, string? municipality = null, string? sortBy = null)
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
                    var dto = await response.Content.ReadFromJsonAsync<PagedListingListDto>();
                    var viewModel = _mapper.Map<ListingPageViewModel>(dto);
                    return viewModel;
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
                return new ListingPageViewModel();
            }
        }
    }
}
