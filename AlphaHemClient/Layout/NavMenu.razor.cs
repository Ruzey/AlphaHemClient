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
        public NavigationManager navigationManager { get; set; }
        private string NavMenuCssClass => collapseNavMenu ? "navmenu mobile-nav-active" : "navmenu";
        private string? realtorId;
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private string? firstName;
        private string? lastName;


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
                (firstName, lastName) = await authService.GetLoggedInUserNames();
            }
            else
            {
                realtorId = null;
                firstName = null;
                lastName = null;
            }

            StateHasChanged(); 
        }
        private void CloseNavMenu()
        {
            collapseNavMenu = false;
        }
        private async Task LogoutAndCloseNavMenu()
        {
            await authService.LogoutAsync();
            collapseNavMenu = false;
            navigationManager.NavigateTo("/");
        }
    }
}

