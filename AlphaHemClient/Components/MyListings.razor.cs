using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace AlphaHemClient.Components
{
    // Author: Conny
    public partial class MyListings
    {
        [Inject]
        private ListingService listingService { get; set; }

        private List<MyListingViewModel> listingsVM = new List<MyListingViewModel>();
        [Parameter]
        public int RealtorId { get; set; }
        private CultureInfo priceFormat;
        protected override async Task OnInitializedAsync()
        {
            listingsVM = await listingService.GetMyListingsAsync(RealtorId);

            // Format the price to Swedish format
            priceFormat = new CultureInfo("sv-SE");
            priceFormat.NumberFormat.NumberGroupSeparator = " ";
            priceFormat.NumberFormat.NumberDecimalSeparator = ",";
            priceFormat.NumberFormat.CurrencyPositivePattern = 3;
        }
    }
}
