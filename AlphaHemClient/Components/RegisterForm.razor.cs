using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
{
    //Author: Mattias
    public partial class RegisterForm
    {
        private List<AgencNamesViewModel> Agencies = new();

        [Inject]
        public AgencyService AgencyService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Agencies = await AgencyService.GetAllAgencyNames();
        }
    }
}
