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

        private string errorMessage;
        private RealtorEditViewModel realtorEditVM = new RealtorEditViewModel();

        
        protected override async Task OnInitializedAsync() // Author: ALL
        {
            if (!await authService.AuthorizeUser(Id))
            {
                navigationManager.NavigateTo("/login");
            }
            await LoadRealtor();
        }
        private async Task LoadRealtor()
        {
            if (string.IsNullOrEmpty(Id))
            {
                errorMessage = "Ingen giltig ID angiven.";
                return;
            }
            try
            {
                realtorEditVM = await realtorService.GetRealtorForEditAsync(Id);
            }
            catch (Exception ex)
            {
                errorMessage = $"Fel vid hämtning av mäklare: {ex.Message}";
            }
        }
        private async Task HandleValidSubmit()
        {
            try
            {
                var response = await realtorService.UpdateRealtorAsync(realtorEditVM);
                if (!response)
                {
                    errorMessage = "Uppdateringen misslyckades.";
                }
                else
                {
                    navigationManager.NavigateTo($"/realtor/{realtorEditVM.Id}");
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Fel vid uppdatering av mäklare: {ex.Message}";
            }
        }
    }
}
