using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;

namespace AlphaHemClient.Pages
{
    //Author : Smilla
    public partial class RealtorProfilePage : ComponentBase
    {
        private RealtorProfileViewModel? realtorProfile;
        private bool isLoading = false;
        private bool isError = false;
        private bool isAuthorized = false;

        [Parameter]
        public string Id { get; set; }

        [Inject]
        private RealtorService RealtorService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private AuthService authService { get; set; }

        // Author: Conny
        private void EditProfile()
        {
            navigationManager.NavigateTo($"/editProfile/{Id}");
        }

        //Author: Smilla
        protected override async Task OnParametersSetAsync()
        {
            isLoading = true;
            isError = false;
            try
            {
                isAuthorized = await authService.AuthorizeUser(Id);
                var response = await RealtorService.GetRealtorByIdAsync(Id);
                if (response != null && response.Data != null)
                {
                    realtorProfile = response.Data;
                }
                else
                {
                    isError = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching realtor: {ex.Message}");
                isError = true;
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
