using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Text.Json;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    public class ListingService : BaseHttpService
    {
        private readonly HttpClient _http;
        private readonly IMapper _mapper;
        private readonly JsLoggingService _jsLoggingService;

        public ListingService(HttpClient http, IMapper mapper, JsLoggingService jsLoggingService, ILocalStorageService localStorage) : base(http, localStorage)
        {
            _http = http;
            _mapper = mapper;
            _jsLoggingService = jsLoggingService;
        }

        public async Task<ListingPageViewModel> GetPaginatedListingsAsync(
            int pageIndex = 1,
            int pageSize = 9,
            string? municipality = null,
            string? category = null,
            string? sortBy = null)
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

                if (!string.IsNullOrEmpty(category))
                {
                    queryParams.Add($"category={category}");
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
                    throw new Exception("Kunde inte hämta bostadsdata.");
                }
            }
            catch (Exception ex)
            {
                // Logga och skriv ut fel
                await _jsLoggingService.LogToConsole($"Error fetching listings: {ex.Message}");
                return new ListingPageViewModel();
            }
        }

        // Author: Conny
        public async Task<List<MyListingViewModel>> GetMyListingsAsync(string id)
        {
            var response = await _http.GetFromJsonAsync<List<ListingListDto>>($"api/listing/realtor/{id}");
            if (response == null)
                return new List<MyListingViewModel>();

            var listingsVM = _mapper.Map<List<MyListingViewModel>>(response);
            return listingsVM;
        }

        //Author : Dominika
        // Co-author: Christoffer, Mattias, Conny
        public async Task CreateListingAsync(ListingCreateViewModel listingVM)
        {
            await GetBearerToken();
            var listingDto = _mapper.Map<ListingCreateDto>(listingVM);
            var response = await _http.PostAsJsonAsync("/api/Listing", listingDto);
            response.EnsureSuccessStatusCode();
        }

        //Author: Dominika
        // Co-author: ALL
        public async Task UpdateListingAsync(int id, ListingUpdateViewModel listingVM)
        {
            await GetBearerToken();
            var listingDto = _mapper.Map<ListingUpdateDto>(listingVM);
            var response = await _http.PutAsJsonAsync($"/api/Listing/{id}", listingDto);

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
        public async Task<ListingUpdateViewModel> GetListingByIdAsync(string id)
        {
            var response = await _http.GetFromJsonAsync<ListingDetailsDto>($"/api/Listing/{id}");
            if (response != null)
                return _mapper.Map<ListingUpdateViewModel>(response);

            return null;
        }

        //Author: Niklas
        // Co-author: ALL
        public async Task<bool> DeleteListingAsync(int id)
        {
            await GetBearerToken();
            try
            {
                var response = await _http.DeleteAsync($"/api/listing/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await _jsLoggingService.LogToConsole($"Fel vid borttagning av listing {id}: {ex.Message}");
                return false;
            }
        }
    }
}