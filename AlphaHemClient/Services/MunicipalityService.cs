using AlphaHemAPI.Data.DTO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    public class MunicipalityService
    {
        private readonly HttpClient _http;

        public MunicipalityService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MunicipalityListDto>> GetMunicipalitiesAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<MunicipalityListDto>>("api/Municipality");
                return result ?? new List<MunicipalityListDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching municipalities: " + ex.Message);
                return new List<MunicipalityListDto>();
            }

        }
    }
}
