using AlphaHemClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Layout
{
    // Author: Smilla, Christoffer, Mattias, Conny
    public partial class NavMenu
    {

        private bool collapseNavMenu = true;

        [Inject]
        public AuthService authService { get; set; }
        [Inject]
        private ILocalStorageService localStorage { get; set; }

        private string? NavMenuCssClass => collapseNavMenu ? "mobile-nav-active" : null;
        private string? realtorId;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async void OnInitialized()
        {
            realtorId = await localStorage.GetItemAsync<string>("userId");
        }
    }
}

