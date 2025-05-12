using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AlphaHemClient.Services
{
    //Author: Mattias
    public class AgencyService
    {
        private readonly HttpClient http;
        private readonly IMapper mapper;

        public AgencyService(HttpClient http, IMapper mapper)
        {
            this.http = http;
            this.mapper = mapper;
        }

        public async Task<List<AgencyNamesViewModel>> GetAllAgencyNames()
        {
            try
            {
                var response = await http.GetFromJsonAsync<List<AgencyNamesViewModel>>("api/agency");

                return response ?? new List<AgencyNamesViewModel>();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching agencies", ex);
            }
        }

        public async Task<List<AgencyVM>> GetAllAgencies()
        {
            try
            {
                var response = await http.GetFromJsonAsync<List<AgencyVM>>("api/agency");
                return response ?? new List<AgencyVM>();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching agencies", ex);
            }
        }

        public async Task<Response<AgencyVM>> GetAgencyById(int id)
        {
            try
            {
                // var response = await _http.GetFromJsonAsync<AgencyVM>($"api/agency/{id}");
                var httpResponse = await http.GetAsync($"api/agency/{id}");
                var content = await httpResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
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
