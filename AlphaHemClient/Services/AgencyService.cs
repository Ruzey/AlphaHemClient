using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace AlphaHemClient.Services
{
    //Author: Mattias
    // Co-author: Conny
    public class AgencyService
    {
        private readonly HttpClient http;
        private readonly IMapper mapper;
        JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Author: Conny

        public AgencyService(HttpClient http, IMapper mapper)
        {
            this.http = http;
            this.mapper = mapper;
        }

        public async Task<Response<List<AgencyNamesViewModel>>> GetAllAgencyNames()
        {
            try
            {
                var httpResponse = await http.GetAsync("api/agency");
                var content = await httpResponse.Content.ReadAsStringAsync();


                if (!httpResponse.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<Response<List<AgencyWithRealtorsDto>>>(content, options);
                    return new Response<List<AgencyNamesViewModel>>
                    {
                        StatusCode = response.StatusCode,
                        Message = response.Message,
                        Errors = response.Errors
                    };
                }

                var agenciesDto = JsonSerializer.Deserialize<List<AgencyWithRealtorsDto>>(content, options);
                var agenciesVM = mapper.Map<List<AgencyNamesViewModel>>(agenciesDto);

                return new Response<List<AgencyNamesViewModel>>
                {
                    StatusCode = httpResponse.StatusCode,
                    Data = agenciesVM
                };
            }
            catch (Exception ex)
            {
                return new Response<List<AgencyNamesViewModel>>
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }
        }

        public async Task<Response<List<AgencyVM>>> GetAllAgencies()
        {
            try
            {
                var httpResponse = await http.GetAsync("api/agency");
                var content = await httpResponse.Content.ReadAsStringAsync();


                if (!httpResponse.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<Response<List<AgencyWithRealtorsDto>>>(content, options);
                    return new Response<List<AgencyVM>>
                    {
                        StatusCode = response.StatusCode,
                        Message = response.Message,
                        Errors = response.Errors
                    };
                }

                var agenciesDto = JsonSerializer.Deserialize<List<AgencyWithRealtorsDto>>(content, options);
                var agenciesVM = mapper.Map<List<AgencyVM>>(agenciesDto);

                return new Response<List<AgencyVM>>
                {
                    StatusCode = httpResponse.StatusCode,
                    Data = agenciesVM
                };
            }
            catch (Exception ex)
            {
                return new Response<List<AgencyVM>>
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }
        }
        // Co-author: Christoffer, Conny
        public async Task<Response<AgencyVM>> GetAgencyById(int id)
        {
            try
            {
                var httpResponse = await http.GetAsync($"api/agency/{id}");
                var content = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var agencyDto = JsonSerializer.Deserialize<AgencyWithRealtorsDto>(content, options);
                    var agencyVM = mapper.Map<AgencyVM>(agencyDto);

                    return new Response<AgencyVM>
                    {
                        Data = agencyVM,
                        StatusCode = HttpStatusCode.OK
                    };
                }
                else
                {
                    var responseAgencyDto = JsonSerializer.Deserialize<Response<AgencyWithRealtorsDto>>(content, options);
                    return new Response<AgencyVM>
                    {
                        StatusCode = responseAgencyDto.StatusCode,
                        Message = responseAgencyDto.Message,
                        Errors = responseAgencyDto.Errors
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<AgencyVM>
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }
        }
    }
}
