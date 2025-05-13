using AlphaHemAPI.Data.DTO;
using AlphaHemClient.HelperClasses;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Json;
namespace AlphaHemClient.Pages
//Author : Niklas
// Co-author: Conny
{
    public partial class ListingDetails
    {
        [Parameter]
        public int id { get; set; }

        private ListingDetailsViewModel? listing;

        [Inject]
        private ListingService listingService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var response = await listingService.GetListingByIdAsync(id);
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            listing = response.Data;
        }
    }
}
