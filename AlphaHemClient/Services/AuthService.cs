using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Providers;
using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace AlphaHemClient.Services
{
    // Author: ALL
    public class AuthService
    {
        private readonly HttpClient http;
        private readonly IMapper map;
        private readonly ILocalStorageService localStorage;
        private readonly AlphaApiAuthenticationStateProvider alphaApiAuthenticationStateProvider;

        public AuthService(HttpClient http, IMapper map, ILocalStorageService localStorage, AlphaApiAuthenticationStateProvider alphaApiAuthenticationStateProvider)
        {
            this.http = http;
            this.map = map;
            this.localStorage = localStorage;
            this.alphaApiAuthenticationStateProvider = alphaApiAuthenticationStateProvider;
        }
        public async Task<string> RegisterAsync(RealtorRegisterVM registerUser)
        {
            try
            {
                var realtor = map.Map<RealtorRegisterDto>(registerUser);
                var response = await http.PostAsJsonAsync("api/Auth/register", realtor);

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = ($"Registrering misslyckades: " + content);
                    return errorMessage;
                }

                return "Registrering lyckades! Du behöver nu vänta på en Admin att godkänna ditt konto";
            }
            catch (Exception ex)
            {
                return $"Registrering misslyckades: {ex.Message}";

            }
        }

        public async Task<string> LoginAsync(RealtorLoginVM loginUser)
        {
            try
            {
                var realtor = map.Map<RealtorLoginDto>(loginUser);
                var response = await http.PostAsJsonAsync("api/Auth/login", realtor);

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = ($"Inloggning misslyckades: " + content);
                    return errorMessage;
                }

                var data = JsonSerializer.Deserialize<UserData>(content);
                await localStorage.SetItemAsync("accessToken", data.Token);

                await alphaApiAuthenticationStateProvider.LoggedIn();
                return data.Token;

            }
            catch (Exception ex)
            {
                return $"Inloggning misslyckades: {ex.Message}";
            }
        }

        public async Task LogoutAsync()
        {
            await alphaApiAuthenticationStateProvider.LoggedOut();
        }
    }
}
