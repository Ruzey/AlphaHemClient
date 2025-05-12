using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
using Blazored.LocalStorage;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace AlphaHemClient.Services
{
    // Author : Smilla
    public class RealtorService : BaseHttpService
    {
        private readonly HttpClient http;
        private readonly IMapper mapper;

        public RealtorService(HttpClient http, IMapper mapper, ILocalStorageService localStorage) : base(http, localStorage)
        {
            this.http = http;
            this.mapper = mapper;
        }

        // Author: Smilla
        // Co-author: ALL
        public async Task<Response<RealtorProfileViewModel>> GetRealtorByIdAsync(string id)
        {
            try
            {
                var httpResponse = await http.GetAsync($"api/realtor/{id}");
                var content = await httpResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var responseDto = JsonSerializer.Deserialize<Response<RealtorDto>>(content, options);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var realtorVM = mapper.Map<RealtorProfileViewModel>(responseDto);

                    return new Response<RealtorProfileViewModel>
                    {
                        Data = realtorVM,
                        StatusCode = httpResponse.StatusCode
                    };
                }
                else
                {
                    return new Response<RealtorProfileViewModel>
                    {
                        StatusCode = responseDto.StatusCode,
                        Message = responseDto.Message,
                        Errors = responseDto.Errors
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<RealtorProfileViewModel>
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }
        }

        // Author: Conny

        // fortsätt här sen
        public async Task<RealtorEditViewModel> GetRealtorForEditAsync(string id)
        {
            try
            {
                var response = await http.GetFromJsonAsync<RealtorDto>($"api/realtor/{id}");
                if (response == null)
                    return null;

                var editRealtorVM = mapper.Map<RealtorEditViewModel>(response);
                return editRealtorVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching realtor with ID {id}: {ex.Message}");
                return null;
            }
        }

        // Author: Conny
        public async Task<bool> UpdateRealtorAsync(RealtorEditViewModel realtorVM)
        {
            var realtorDto = mapper.Map<RealtorUpdateDto>(realtorVM);

            try
            {
                var response = await http.PutAsJsonAsync($"api/realtor/{realtorDto.Id}", realtorDto);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Fel vid uppdatering av mäklare. Status: {(int)response.StatusCode} {response.ReasonPhrase}. Svar: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett oväntat fel uppstod vid uppdatering av mäklare: {ex.Message}");
                return false;
            }
        }

        //Author: ALL
        public async Task<bool> ApproveRealtor(string id)
        {
            await GetBearerToken();
            try
            {
                var httpResponse = await http.PutAsync($"api/Realtor/ApproveRealtor/{id}", null);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        // Author: Conny
        public async Task<Response> DeclineRealtorAsync(string id)
        {
            await GetBearerToken();

            try
            {
                var httpResponse = await http.DeleteAsync($"api/Realtor/{id}");
                if (!httpResponse.IsSuccessStatusCode)
                {
                    var httpResponseContent = await httpResponse.Content.ReadAsStringAsync();

                    // Hantera HTTP returkoder om inte 200-299
                    switch (httpResponse.StatusCode)
                    {
                        case HttpStatusCode.NotFound: // 404
                            return new Response
                            {
                                Message = "Mäklaren kunde inte hittas.",
                                Errors = new List<string> { httpResponseContent },
                                StatusCode = httpResponse.StatusCode
                            };
                        case HttpStatusCode.BadRequest: // 400
                            return new Response
                            {
                                Message = "Fel vid förfrågan om att uppdatera mäklarlistan.",
                                Errors = new List<string> { httpResponseContent },
                                StatusCode = httpResponse.StatusCode
                            };
                        case HttpStatusCode.Unauthorized: // 401
                            return new Response
                            {
                                Message = "Du har inte behörighet att genomföra denna åtgärd.",
                                Errors = new List<string> { httpResponseContent },
                                StatusCode = httpResponse.StatusCode
                            };
                    }
                }

                // Skicka tillbaka HTTP returkod om allt går bra
                return new Response
                {
                    StatusCode = httpResponse.StatusCode // 204
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}" },
                    StatusCode = HttpStatusCode.InternalServerError // 500
                };
            }
        }
    }
}