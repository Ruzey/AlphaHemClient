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
        string LoginMessage = string.Empty;

        [Inject]
        public AuthService AuthService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        public async Task HandleLogin()
        {
            LoginMessage = await AuthService.LoginAsync(LoginModel);
            StateHasChanged();
            //await Task.Delay(3000);
            //navigationManager.NavigateTo("/");
        }
    }
}
