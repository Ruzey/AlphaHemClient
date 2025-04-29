using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace AlphaHemClient.Components
{
    // Author: Conny
    // Co-Author: Niklas
    public partial class MyListings
    {
        [Inject]
        private ListingService listingService { get; set; }

        private List<MyListingViewModel> listingsVM = new List<MyListingViewModel>();
        [Parameter]
        public int RealtorId { get; set; }
        private CultureInfo priceFormat;
        private int loggedInRealtorId;


        private bool showConfirmModal = false;
        private int listingIdToDelete;
        protected override async Task OnInitializedAsync()
        {
            //Prep inför identity
            //loggedInRealtorId = WHATEVER;
            listingsVM = await listingService.GetMyListingsAsync(RealtorId);

            // Format the price to Swedish format
            priceFormat = new CultureInfo("sv-SE");
            priceFormat.NumberFormat.NumberGroupSeparator = " ";
            priceFormat.NumberFormat.NumberDecimalSeparator = ",";
            priceFormat.NumberFormat.CurrencyPositivePattern = 3;
        }
        private void AskForDeleteConfirmation(int id)
        {
            listingIdToDelete = id;
            showConfirmModal = true;
        }
        private async Task ConfirmDeleteAsync()
        {
            showConfirmModal = false;

            var success = await listingService.DeleteListingAsync(listingIdToDelete);
            if (success)
            {
                var item = listingsVM.FirstOrDefault(l => l.Id == listingIdToDelete);
                if (item != null)
                {
                    listingsVM.Remove(item);
                }
            }
        }
    }
}
