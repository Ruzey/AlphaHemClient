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
        private bool isAuthorized = false;
        protected override async Task OnInitializedAsync()
        {
            var listing = await listingService.GetListingByIdAsync(ListingId);
            isAuthorized = await authService.AuthorizeUser(listing.RealtorId);
        }

        private void NavigateToEdit()
        {
            NavigationManager.NavigateTo($"/editlisting/{ListingId}");
        }

    }
}
