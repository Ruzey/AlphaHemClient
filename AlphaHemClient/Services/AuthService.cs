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

        public async Task<string?> GetAccessTokenAsync()
        {
            return await localStorage.GetItemAsync<string>("accessToken");
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

        public async Task<bool> LoginAsync(RealtorLoginVM loginUser)
        {
            try
            {
                var realtor = map.Map<RealtorLoginDto>(loginUser);
                var response = await http.PostAsJsonAsync("api/Auth/login", realtor);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var data = JsonSerializer.Deserialize<UserData>(content);
                await localStorage.SetItemAsync("accessToken", data.Token);
                await localStorage.SetItemAsync("email", data.Email);
                await localStorage.SetItemAsync("userId", data.UserId);
                await localStorage.SetItemAsync("firstName", data.FirstName);
                await localStorage.SetItemAsync("lastName", data.LastName);

                await alphaApiAuthenticationStateProvider.LoggedIn();

                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetLoggedInUserId()
        {
            var userId = await localStorage.GetItemAsync<string>("userId");
            return userId;
        }
        public async Task<(string?, string?)> GetLoggedInUserNames()
        { 
            return (await localStorage.GetItemAsync<string>("firstName"), await localStorage.GetItemAsync<string>("lastName"));
        }
        public async Task LogoutAsync()
        {
            await alphaApiAuthenticationStateProvider.LoggedOut();
        }

        public async Task<bool> AuthorizeUser(string entityId)
        {
            var userId = await localStorage.GetItemAsync<string>("userId");
            return userId == entityId;
        }
    }
}
