using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace AlphaHemClient.Pages
{
    // Author: Conny
    public partial class EditProfile
    {
        [Inject] private RealtorService realtorService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private AuthService authService { get; set; } // Author: ALL
        [Parameter] public string Id { get; set; }
        private string errorMessage = "";
        private RealtorUpdateViewModel realtorUpdateVM = new RealtorUpdateViewModel();


        protected override async Task OnInitializedAsync() // Author: ALL
        {
            if (!await authService.AuthorizeUser(Id))
            {
                navigationManager.NavigateTo("/403-Forbidden");
                return;
            }

            await LoadRealtor();
        }
        private async Task LoadRealtor()
        {
            var response = await realtorService.GetRealtorForUpdateAsync(Id);
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            realtorUpdateVM = response.Data;
        }
        private async Task HandleValidSubmit()
        {
            var response = await realtorService.UpdateRealtorAsync(realtorUpdateVM);
            var page = NavHandler.Handler(response.StatusCode);
            if (page == null)
            {
                navigationManager.NavigateTo($"/realtor/{realtorUpdateVM.Id}");
                return;
            }
            errorMessage = $"Kunde inte uppdatera mäklare : {response.Errors.FirstOrDefault()}";
        }
    }
}
