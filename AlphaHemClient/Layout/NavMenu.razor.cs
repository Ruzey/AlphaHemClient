using System.Security.Claims;
using AlphaHemClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AlphaHemClient.Layout
{
    // Author: Smilla, Christoffer, Mattias, Conny
    // Co-Author: ALL
    public partial class NavMenu
    {

        private bool collapseNavMenu = true;

        [Inject]
        public AuthService authService { get; set; }
        [Inject]
        private ILocalStorageService localStorage { get; set; }

        private string? NavMenuCssClass => collapseNavMenu ? "mobile-nav-active" : null;
        private string? realtorId;
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationStateTask { get; set; }


        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async Task OnParametersSetAsync()
        {
            var authState = await AuthenticationStateTask;
            ClaimsPrincipal user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                realtorId = user.FindFirst("uid")?.Value;
            }
            else
            {
                realtorId = null;
            }

            StateHasChanged(); 
        }

    }
}

