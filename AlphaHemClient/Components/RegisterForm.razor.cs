using AlphaHemAPI.Data.DTO;
using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace AlphaHemClient.Components
{
    //Author: Mattias
    // Co-Author: All

    public partial class RegisterForm
    {
        private RealtorRegisterVM RegisterModel = new RealtorRegisterVM();
        string message = string.Empty;
        string messageName = string.Empty;
        private List<AgencyNamesViewModel> Agencies = new();

        [Inject]
        public AgencyService AgencyService { get; set; }
        [Inject]
        public AuthService AuthService { get; set; }
        [Inject] 
        private NavigationManager navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var response = await AgencyService.GetAllAgencyNames();
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            Agencies = response.Data;
        }

        public async Task ErrorRegister()
        {
            message = "Ett fel inträffade vid registrering. Försök igen.";
        }
        public async Task HandleRegister()
        {
                message = await AuthService.RegisterAsync(RegisterModel);
                StateHasChanged();
                await Task.Delay(3000);
                navigationManager.NavigateTo("/");
        }
    }
}
