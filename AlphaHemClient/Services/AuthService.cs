using System.Diagnostics;
using System.Net.Http.Json;
using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;

namespace AlphaHemClient.Services
{
    // Author: ALL
    public class AuthService
    {
        private readonly HttpClient http;
        private readonly IMapper map;

        public AuthService(HttpClient http, IMapper map)
        {
            this.http = http;
            this.map = map;
        }
        public async Task<string> RegisterAsync(RealtorRegisterVM registerUser)
        {

            var realtor = map.Map<RealtorRegisterDto>(registerUser);
            var response = await http.PostAsJsonAsync("api/Auth/register", realtor);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = ($"Registrering misslyckades: " + content);
                return errorMessage;
            }

            return "Registrering lyckades!";


        }
    }
}
