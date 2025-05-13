using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AlphaHemClient.Pages
{  
    //Author: Mattias
    public partial class EditAgency
    {
        [Inject] private AgencyService agencyService { get; set; }
        [Inject] private RealtorService realtorService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private AuthService authService { get; set; }
        [Inject] private AuthenticationStateProvider authProvider { get; set; }
        [Parameter] public int Id { get; set; }
        private bool sameAgencyAndAdmin = false;
        private string errorMessage = "";
        private AgencyVM agencyUpdate = new AgencyVM();

        protected override async Task OnInitializedAsync()
        {
            var response = await agencyService.GetAgencyById(Id);
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            agencyUpdate = response.Data;
            
            var currentUserId = await authService.GetLoggedInUserId();
            var responseOfRealtor = await realtorService.GetRealtorByIdAsync(currentUserId);

            var authState = await authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            bool isAdmin = user.IsInRole("RealtorAdmin");

            if (responseOfRealtor.Data.AgencyId != agencyUpdate.Id || !isAdmin)
            {
                navigationManager.NavigateTo("/403-Forbidden");
                return;
            }
            sameAgencyAndAdmin = true;
        }

        private async Task HandleValidSubmit()
        {
            var response = await agencyService.UpdateAgencyAsync(agencyUpdate);
            var page = NavHandler.Handler(response.StatusCode);
            if (page == null)
            {
                navigationManager.NavigateTo($"/agency/{agencyUpdate.Id}");
                return;
            }
            errorMessage = $"Kunde inte uppdatera byrå : {response.Errors.FirstOrDefault()}";
        }
    }
}
