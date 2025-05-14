using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace AlphaHemClient.Components
{
    // Author: Conny
    // Co-Author: ALL
    public partial class MyListings
    {
        [Inject]
        private ListingService listingService { get; set; }
        [Inject]
        private AuthService authService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        private bool IsAuthorized;

        private List<MyListingViewModel> listingsVM = new List<MyListingViewModel>();
        [Parameter]
        public string RealtorId { get; set; }


        private bool showConfirmModal = false;
        private int listingIdToDelete;
        protected override async Task OnInitializedAsync()
        {
            IsAuthorized = await authService.AuthorizeUser(RealtorId);
            listingsVM = await listingService.GetMyListingsAsync(RealtorId);
        }
        private void AskForDeleteConfirmation(int id)
        {
            listingIdToDelete = id;
            showConfirmModal = true;
        }
        private async Task ConfirmDeleteAsync()
        {
            showConfirmModal = false;

            var response = await listingService.DeleteListingAsync(listingIdToDelete);
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            var listingToDelete = listingsVM.FirstOrDefault(l => l.Id == listingIdToDelete);
            if (listingToDelete != null)
                listingsVM.Remove(listingToDelete);
        }
    }
}
