using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
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
        JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Author: Conny


        public RealtorService(HttpClient http, IMapper mapper, AuthService authService) : base(http, authService)
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

                if (httpResponse.IsSuccessStatusCode)
                {
                    var realtorDto = JsonSerializer.Deserialize<RealtorDto>(content, options);
                    var realtorVM = mapper.Map<RealtorProfileViewModel>(realtorDto);

                    return new Response<RealtorProfileViewModel>
                    {
                        Data = realtorVM,
                        StatusCode = httpResponse.StatusCode
                    };
                }
                else
                {
                    var responseRealtorDto = JsonSerializer.Deserialize<Response<RealtorProfileViewModel>>(content, options);
                    return new Response<RealtorProfileViewModel>
                    {
                        StatusCode = responseRealtorDto.StatusCode,
                        Message = responseRealtorDto.Message,
                        Errors = responseRealtorDto.Errors
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
        public async Task<Response<RealtorUpdateViewModel>> GetRealtorForUpdateAsync(string id)
        {
            try
            {
                var httpResponse = await http.GetAsync($"api/realtor/{id}");
                var content = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var realtorDto = JsonSerializer.Deserialize<RealtorDto>(content, options);
                    var realtorVM = mapper.Map<RealtorUpdateViewModel>(realtorDto);

                    return new Response<RealtorUpdateViewModel>
                    {
                        Data = realtorVM,
                        StatusCode = httpResponse.StatusCode
                    };
                }
                else
                {
                    var responseRealtorDto = JsonSerializer.Deserialize<Response<RealtorUpdateViewModel>>(content, options);
                    return new Response<RealtorUpdateViewModel>
                    {
                        StatusCode = responseRealtorDto.StatusCode,
                        Message = responseRealtorDto.Message,
                        Errors = responseRealtorDto.Errors
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<RealtorUpdateViewModel>
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }
        }

        // Author: Conny
        public async Task<Response> UpdateRealtorAsync(RealtorUpdateViewModel realtorVM)
        {
            try
            {
                await GetBearerToken();

                var realtorDto = mapper.Map<RealtorUpdateDto>(realtorVM);
                var httpResponse = await http.PutAsJsonAsync($"api/realtor/{realtorDto.Id}", realtorDto);

                if (httpResponse.IsSuccessStatusCode)
                    return new Response { StatusCode = httpResponse.StatusCode };

                var content = await httpResponse.Content.ReadAsStringAsync();
                var responseDto = JsonSerializer.Deserialize<Response>(content, options);
                return new Response
                {
                    StatusCode = responseDto.StatusCode,
                    Message = responseDto.Message,
                    Errors = responseDto.Errors
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }
        }

        //Author: ALL
        public async Task<Response> ApproveRealtor(string id)
        {
            await GetBearerToken();
            try
            {
                var httpResponse = await http.PutAsync($"api/Realtor/ApproveRealtor/{id}", null);

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
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }

        }

        // Author: Conny
        // Co-author: Mattias, Christoffer
        public async Task<Response> DeclineRealtorAsync(string id)
        {
            await GetBearerToken();

            try
            {
                var httpResponse = await http.DeleteAsync($"api/Realtor/{id}");
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