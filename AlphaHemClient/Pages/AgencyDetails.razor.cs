using System.Net;
using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.DTO;
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
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public RealtorService realtorService { get; set; }
        private AgencyVM? agency { get; set; }

        // Co-author: ALL
        protected override async Task OnInitializedAsync()
        {
            var response = await agencyService.GetAgencyById(id);

            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }

            agency = response.Data;

            loggedInUserId = await authService.GetLoggedInUserId();
            var realtor = await realtorService.GetRealtorByIdAsync(loggedInUserId);
            if (realtor == null)
            {
                return;
            }

            if (agency.Name == realtor.AgencyName)
                sameAgency = true;
        }

        public async Task ApproveRealtor(string id)
        {
            var confirmed = await realtorService.ApproveRealtor(id);
            if (confirmed)
            {
                agency.Realtors.FirstOrDefault(r => r.Id == id).EmailConfirmed = true;
            }
        }

        // Author: Conny
        public async Task DeclineRealtor(string id)
        {
            Response response = await realtorService.DeclineRealtorAsync(id);

            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }

            var realtor = agency?.Realtors.FirstOrDefault(r => r.Id == id);
            if (realtor != null)
                agency?.Realtors.Remove(realtor);
        }
    }
}
