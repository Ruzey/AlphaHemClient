using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
using System.Net.Http.Json;
using System.Text.Json;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    public class ListingService
    {
        private readonly HttpClient _http;
        private readonly IMapper _mapper;
        private readonly JsLoggingService _jsLoggingService;

        public ListingService(HttpClient http, IMapper mapper, JsLoggingService jsLoggingService)
        {
            _http = http;
            _mapper = mapper;
            _jsLoggingService = jsLoggingService;
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

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var dto = JsonSerializer.Deserialize<PagedListingListDto>(responseContent, options);

                    var viewModel = _mapper.Map<ListingPageViewModel>(dto);

                    return viewModel;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await _jsLoggingService.LogToConsole($"Fel vid API-anrop, status: {response.StatusCode}, error: {error}");
                    Console.WriteLine($"Tekniskt fel: {error}");
                    throw new Exception("Kunde inte hämta bostadsdata.");
                }
            }
            catch (Exception ex)
            {
                // Logga och skriv ut fel
                await _jsLoggingService.LogToConsole($"Error fetching listings: {ex.Message}");
                Console.WriteLine($"Error fetching listings: {ex.Message}");
                return new ListingPageViewModel();
            }
        }

        // Author: Conny
        public async Task<List<MyListingViewModel>> GetMyListingsAsync(int id)
        {
            var response = await _http.GetFromJsonAsync<List<ListingListDto>>($"api/listing/realtor/{id}");
            if (response == null)
                return new List<MyListingViewModel>();

            var listingsVM = _mapper.Map<List<MyListingViewModel>>(response);
            Console.WriteLine("calling service method...");
            return listingsVM;
        }

        //Author : Dominika
        public async Task CreateListingAsync(ListingCreateDto listing)
        {
            var response = await _http.PostAsJsonAsync("/api/Listing", listing);
            response.EnsureSuccessStatusCode();
        }

        //Author: Dominika
        public async Task UpdateListingAsync(string id, ListingUpdateDto listing)
        {
            var response = await _http.PutAsJsonAsync($"/api/Listing/{id}", listing);

            if (response.IsSuccessStatusCode)
            {
                if (response.Content.Headers.ContentLength > 0)
                {
                    await response.Content.ReadFromJsonAsync<ListingDetailsDto>();
                }
                else
                {
                    Console.WriteLine("Uppdateringen lyckades, men inget innehåll returnerades.");
                }
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Fel vid uppdatering av bostad: {error}");
            }
        }

        //Author: Dominika
        public async Task<ListingUpdateDto> GetListingByIdAsync(string id)
        {
            var response = await _http.GetFromJsonAsync<ListingUpdateDto>($"/api/Listing/{id}");
            return response;
        }


    }
}