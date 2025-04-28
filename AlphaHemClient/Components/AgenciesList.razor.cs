using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Components
{
    //Author: Mattias
    public partial class AgenciesList
    {
        private List<AgencyVM> AllAgenciesList = new();

        [Inject]
        public AgencyService AgencyService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            AllAgenciesList = await AgencyService.GetAllAgencies();
        }
    }
}
