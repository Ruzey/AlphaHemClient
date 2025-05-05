using AlphaHemAPI.Data.DTO;
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
            Agencies = await AgencyService.GetAllAgencyNames();
        }

        public async Task ErrorRegister()
        {
            // TODO: Byt färg på error texten, fungrar inte med alert alert-danger
            //var messageName = "alert alert-danger";
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
