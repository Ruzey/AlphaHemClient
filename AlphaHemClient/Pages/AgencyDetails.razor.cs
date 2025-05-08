using System.Net;
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

        protected override async Task OnInitializedAsync()
        {
            agency = await agencyService.GetAgencyById(id);
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
        // Radera mäklare om vi inte vill ha kvar dem på våran byrå

        // Author: Conny
        public async Task DeclineRealtor(string id)
        {
            Response response = await realtorService.DeclineRealtorAsync(id);
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    navigationManager.NavigateTo("/400-BadRequest");
                    break;

                case HttpStatusCode.Unauthorized:
                    navigationManager.NavigateTo("/401-Unauthorized");
                    break;

                case HttpStatusCode.NotFound:
                    navigationManager.NavigateTo("/404-NotFound");
                    break;

                case HttpStatusCode.InternalServerError:
                    navigationManager.NavigateTo("/500-InternalServerError");
                    break;

                default: // 204 NoContent
                    var realtor = agency?.Realtors.FirstOrDefault(r => r.Id == id);
                    if (realtor != null)
                        agency?.Realtors.Remove(realtor);
                    break;
            }
        }
    }
}
