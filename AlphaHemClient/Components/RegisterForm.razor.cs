using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
{
    //Author: Mattias
    // Co-Author: All

    public partial class RegisterForm
    {
        private RealtorRegisterDto RegisterModel = new();

        private List<AgencyNamesViewModel> Agencies = new();

        [Inject]
        public AgencyService AgencyService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Agencies = await AgencyService.GetAllAgencyNames();
        }

        public async Task HandleRegister()
        {

        }
    }
}
