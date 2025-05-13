using AlphaHemClient.Model.DTO;
using AutoMapper;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using AlphaHemClient.Model.ViewModel;
using System.Text.Json;
using System.Net;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    public class MunicipalityService
    {
        private readonly HttpClient http;
        private readonly IMapper mapper;

        public MunicipalityService(HttpClient http, IMapper mapper)
        {
            this.http = http;
            this.mapper = mapper;
        }

        public async Task<Response<List<MunicipalityViewModel>>> GetMunicipalitiesAsync()
        {
            try
            {
                var httpResponse = await http.GetAsync("api/Municipality");
                var content = await httpResponse.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                if (!httpResponse.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<Response<List<MunicipalityListDto>>>(content, options);
                    return new Response<List<MunicipalityViewModel>>
                    {
                        StatusCode = response.StatusCode,
                        Message = response.Message,
                        Errors = response.Errors
                    };
                }

                var municipalitiesDto = JsonSerializer.Deserialize<List<MunicipalityListDto>>(content, options);
                var municipalitiesVM = mapper.Map<List<MunicipalityViewModel>>(municipalitiesDto);

                return new Response<List<MunicipalityViewModel>>
                {
                    StatusCode = httpResponse.StatusCode,
                    Data = municipalitiesVM
                };
            }
            catch (Exception ex)
            {
                return new Response<List<MunicipalityViewModel>>
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Message = "Ett oväntat fel har uppstått.",
                    Errors = new List<string> { $"Felmeddelande: {ex.Message}." }
                };
            }

        }
    }
}
