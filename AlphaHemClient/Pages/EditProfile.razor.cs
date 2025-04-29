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
        [Parameter] public int Id { get; set; }

        private string errorMessage;
        private RealtorEditViewModel realtorEditVM = new RealtorEditViewModel();

        protected override async Task OnInitializedAsync()
        {
            await LoadRealtor();
        }
        private async Task LoadRealtor()
        {
            if (Id == 0)
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
