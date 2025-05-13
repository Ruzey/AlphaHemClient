using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    // Author : Niklas
    public class ListingService : BaseHttpService
    {
        private readonly HttpClient http;
        private readonly IMapper mapper;
        private readonly JsLoggingService jsLoggingService;
        JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Author: Conny

        public ListingService(HttpClient http, IMapper mapper, JsLoggingService jsLoggingService, AuthService authService) : base(http, authService) // Author : Niklas
        {
            this.http = http;
            this.mapper = mapper;
            this.jsLoggingService = jsLoggingService;
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

                var response = await http.GetAsync(url);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var dto = JsonSerializer.Deserialize<PagedListingListDto>(responseContent, options);

                    var viewModel = mapper.Map<ListingPageViewModel>(dto);

                    return viewModel;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await jsLoggingService.LogToConsole($"Fel vid API-anrop, status: {response.StatusCode}, error: {error}");
                    throw new Exception("Kunde inte hämta bostadsdata.");
                }
            }
            catch (Exception ex)
            {
                // Logga och skriv ut fel
                await jsLoggingService.LogToConsole($"Error fetching listings: {ex.Message}");
                return new ListingPageViewModel();
            }
        }

        // Author: Conny
        public async Task<Response<ListingDetailsViewModel>> GetListingByIdAsync(int id)
        {
            try
            {
                var httpResponse = await http.GetAsync($"api/listing/{id}");
                var content = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var listingDto = JsonSerializer.Deserialize<ListingDetailsDto>(content, options);
                    var listingVM = mapper.Map<ListingDetailsViewModel>(listingDto);
                    return new Response<ListingDetailsViewModel>
                    {
                        StatusCode = httpResponse.StatusCode,
                        Data = listingVM
                    };
                }
                var responseDto = JsonSerializer.Deserialize<Response<ListingDetailsDto>>(content, options);
                return new Response<ListingDetailsViewModel>
                {
                    StatusCode = responseDto.StatusCode,
                    Message = responseDto.Message,
                    Errors = responseDto.Errors
                };
            }
            catch (Exception ex)
            {
                return new Response<ListingDetailsViewModel>
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }
        }

        // Author: Conny
        public async Task<List<MyListingViewModel>> GetMyListingsAsync(string id)
        {
            var response = await http.GetFromJsonAsync<List<ListingListDto>>($"api/listing/realtor/{id}");
            if (response == null)
                return new List<MyListingViewModel>();

            var listingsVM = mapper.Map<List<MyListingViewModel>>(response);
            return listingsVM;
        }

        //Author : Dominika
        // Co-author: Christoffer, Mattias, Conny
        public async Task<Response> CreateListingAsync(ListingCreateViewModel listingVM)
        {
            try
            {
                await GetBearerToken();
                var listingDto = mapper.Map<ListingCreateDto>(listingVM);

                var httpResponse = await http.PostAsJsonAsync("/api/Listing", listingDto);
                var content = await httpResponse.Content.ReadAsStringAsync();

                var response = JsonSerializer.Deserialize<Response>(content, options);
                if (httpResponse.IsSuccessStatusCode)
                    return new Response { StatusCode = response.StatusCode };

                return new Response
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                    Errors = response.Errors
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}" }
                };
            }
        }

        //Author: Dominika
        // Co-author: ALL
        public async Task<Response> UpdateListingAsync(int id, ListingUpdateViewModel listingVM)
        {
            try
            {
                await GetBearerToken();

                var listingDto = mapper.Map<ListingUpdateDto>(listingVM);
                var httpResponse = await http.PutAsJsonAsync($"api/Listing/{id}", listingDto);
                var content = await httpResponse.Content.ReadAsStringAsync();



                if (httpResponse.IsSuccessStatusCode)
                    return new Response { StatusCode = httpResponse.StatusCode };

                var response = JsonSerializer.Deserialize<Response>(content, options);
                return new Response
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                    Errors = response.Errors
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}" }
                };
            }
        }

        //Author: Dominika
        // Co-author: Conny
        public async Task<Response<ListingUpdateViewModel>> GetListingForUpdateAsync(int id)
        {
            try
            {
                var httpResponse = await http.GetAsync($"api/Listing/{id}");
                var content = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    var responseDto = JsonSerializer.Deserialize<Response<ListingDetailsDto>>(content, options);
                    return new Response<ListingUpdateViewModel>
                    {
                        StatusCode = responseDto.StatusCode,
                        Message = responseDto.Message,
                        Errors = responseDto.Errors
                    };
                }

                var listingDto = JsonSerializer.Deserialize<ListingDetailsDto>(content, options);
                var listingVM = mapper.Map<ListingUpdateViewModel>(listingDto);
                return new Response<ListingUpdateViewModel>
                {
                    StatusCode = httpResponse.StatusCode,
                    Data = listingVM
                };

            }
            catch (Exception ex)
            {
                return new Response<ListingUpdateViewModel>
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }
        }

        //Author: Niklas
        // Co-author: ALL
        public async Task<Response> DeleteListingAsync(int id)
        {
            try
            {
                await GetBearerToken();
                var httpResponse = await http.DeleteAsync($"api/listing/{id}");
                if (httpResponse.IsSuccessStatusCode)
                    return new Response { StatusCode = httpResponse.StatusCode };

                var content = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<Response>(content, options);
                return new Response
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                    Errors = response.Errors
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}" },
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}