using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
{
    //Author: Mattias
    // Co-Author: All
    public partial class LoginForm
    {
        RealtorLoginVM LoginModel = new RealtorLoginVM();
        string realtorId = string.Empty;
        string loginMessage = string.Empty;
        bool loginState = false;
        [Inject]
        public AuthService AuthService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        public async Task HandleLogin()
        {
            loginState = await AuthService.LoginAsync(LoginModel);
            if (loginState)
            {
                realtorId = await AuthService.GetLoggedInUserId();
                navigationManager.NavigateTo($"/realtor/{realtorId}");
            }
            else
            {
                loginMessage = "Inloggning misslyckades, kontrollera dina inloggningsuppgifter.";
            }
        }
    }
}
