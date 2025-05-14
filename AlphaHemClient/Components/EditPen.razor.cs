using AlphaHemClient.HelperClasses;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
// Author: Dominika
// Co-author: Smilla
namespace AlphaHemClient.Components
{
    public partial class EditPen
    {
        [Parameter]
        public int ListingId { get; set; }

        [Parameter]
        public string Class { get; set; }
        [Inject]
        public ListingService listingService { get; set; }
        [Inject]
        public AuthService authService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        private bool isAuthorized = false;
        protected override async Task OnInitializedAsync()
        {
            var response = await listingService.GetListingByIdAsync(ListingId);
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            var realtorId = response.Data.Realtor.Id;
            isAuthorized = await authService.AuthorizeUser(realtorId);
        }

        private void NavigateToEdit()
        {
            NavigationManager.NavigateTo($"/editlisting/{ListingId}");
        }

    }
}
