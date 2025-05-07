using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Pages
{
    //Author: Mattias
    //Co-author: ALL
    public partial class AgencyDetails
    {
        [Parameter]
        public int id { get; set; }
        public string loggedInUserId { get; set; } = string.Empty;
        private bool sameAgency = false;
        [Inject]
        public AgencyService agencyService { get; set; }
        [Inject]
        public AuthService authService { get; set; }
        [Inject]
        public RealtorService realtorService { get; set; }
        private AgencyVM? Agency { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Agency = await agencyService.GetAgencyById(id);
            loggedInUserId = await authService.GetLoggedInUserId();
            var realtor = await realtorService.GetRealtorByIdAsync(loggedInUserId);
            if (realtor == null)
            {
                return;
            }

            if (Agency.Name == realtor.AgencyName)
                sameAgency = true;

        }

        public async Task ApproveRealtor(string id)
        {
            var confirmed = await realtorService.ApproveRealtor(id);
            if (confirmed)
            {
                Agency.Realtors.FirstOrDefault(r => r.Id == id).EmailConfirmed = true;
            }
        }
        // Radera mäklare om vi inte vill ha kvar dem på våran byrå
        public async Task DeclineRealtor(string id)
        {
            //await realtorService.DeleteRealtor(id);
        }
    }
}
