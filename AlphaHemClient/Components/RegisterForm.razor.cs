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

        public async Task HandleRegister()
        {
                message = await AuthService.RegisterAsync(RegisterModel);
        }
    }
}
