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
            var agencyResponse = await agencyService.GetAgencyById(id);

            var page = NavHandler.Handler(agencyResponse.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }

            agency = agencyResponse.Data;

            loggedInUserId = await authService.GetLoggedInUserId();
            if (loggedInUserId == null)
                return;

            var realtorResponse = await realtorService.GetRealtorByIdAsync(loggedInUserId);
            page = NavHandler.Handler(realtorResponse.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            if (agency.Name == realtorResponse.Data.AgencyName)
                sameAgency = true;
        }

        public async Task ApproveRealtor(string id)
        {

            var response = await realtorService.ApproveRealtor(id);
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            var realtor = agency.Realtors.FirstOrDefault(r => r.Id == id);
            if (realtor != null)
                realtor.EmailConfirmed = true;
        }

        // Author: Conny
        // Co-author: Christoffer, Mattias
        public async Task DeclineRealtor(string id)
        {
            var response = await realtorService.DeclineRealtorAsync(id);

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
