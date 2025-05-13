using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using AlphaHemClient.HelperClasses;

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

            isAuthorized = await authService.AuthorizeUser(Id);
            var response = await RealtorService.GetRealtorByIdAsync(Id);
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            realtorProfile = response.Data;
            
            isLoading = false;
        }
    }
}
