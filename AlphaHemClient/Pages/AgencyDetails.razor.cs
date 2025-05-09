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
            /*
                Slutade här i fredags. Vi började på att skapa en helper klass för hantering av http returkoder.
                Denna helpermetod ska returnera en null string när statuskod är mellan 200 och 299, annars 
                returnerar den en string som motsvarar korrekt error-sida som ska användas med navigation manager.
                Poängen är att få bort så många switch-statements som möjligt från våra code-behinds (och eventuellt
                från controllers i API:t).

                Vid mån av tid ska vi fixa så att error-sidorna kan ta emot felmeddelanden som kan visas upp så
                att användaren tydligt ser vad som blev fel.
            */
            
            var response = await agencyService.GetAgencyById(id);
            var page = StatusCodeHandler.Handler(response.StatusCode);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    agency = response.Data;
                    break;
                case HttpStatusCode.NotFound:
                    navigationManager.NavigateTo(page);
                    return;
                case HttpStatusCode.InternalServerError:
                    navigationManager.NavigateTo("/500-InternalServerError");
                    return;
                case HttpStatusCode.ServiceUnavailable:
                    navigationManager.NavigateTo("/503-ServiceUnavailable");
                    return;
            }

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
