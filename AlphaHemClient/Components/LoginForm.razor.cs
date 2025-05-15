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
        string loginMessageClass = string.Empty;
        bool loginState = false;
        [Inject]
        public AuthService AuthService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        public async Task HandleLogin()
        {
            var responseLoginState = await AuthService.LoginAsync(LoginModel);

            if ((int)responseLoginState.StatusCode == 200)
            {
                loginMessage = "Inloggning lyckades!";
                loginMessageClass = "alert alert-success";

                StateHasChanged();
                await Task.Delay(1000); //delay så man hinner läsa meddelandet

                realtorId = await AuthService.GetLoggedInUserId();
                navigationManager.NavigateTo($"/realtor/{realtorId}");
            }
            else
            {
                loginMessage = responseLoginState.Message;
                loginMessageClass = "alert alert-danger";
            }

            StateHasChanged();
        }
    }
}
