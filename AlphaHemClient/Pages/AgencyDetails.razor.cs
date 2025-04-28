using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Pages
{
    //Author: Mattias
    public partial class AgencyDetails
    {
        [Parameter]
        public int id { get; set; }
        [Inject]
        public AgencyService AgencyService { get; set; }
        private AgencyVM? Agency { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Agency = await AgencyService.GetAgencyById(id);
        }
    }
}
