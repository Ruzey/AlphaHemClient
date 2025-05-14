using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaHemClient.Pages
{
    //Author: Dominika
    // Co-author: Conny
    public partial class EditListing
    {
        [Parameter] public int Id { get; set; }
        private ListingUpdateViewModel listing = new ListingUpdateViewModel { Images = new List<string>() };
        private ListingUpdateViewModel tempListing = new ListingUpdateViewModel { Images = new List<string>() };
        private string imageUrl { get; set; } = string.Empty;
        private string errorMessage = "";
        [Inject] private ListingService listingService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private AuthService authService { get; set; }



        private async Task LoadListing()
        {
            var response = await listingService.GetListingForUpdateAsync(Id);
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            tempListing = response.Data;
        }

        private void AddImage()
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                listing.Images.Add(imageUrl);
                imageUrl = string.Empty;
            }
        }

        private void RemoveImage(string image)
        {
            listing.Images.Remove(image);
        }

        // Co-author: ALL
        private async Task HandleValidSubmit()
        {
            var response = await listingService.UpdateListingAsync(Id, listing);
            var page = NavHandler.Handler(response.StatusCode);
            if (page == null)
            {
                navigationManager.NavigateTo($"/listings/{Id}");
                return;
            }
            errorMessage = $"Kunde inte uppdatera bostad : {response.Errors.FirstOrDefault()}";
        }

        // Co-author: ALL
        protected override async Task OnInitializedAsync()
        {
            await LoadListing();
            if (!await authService.AuthorizeUser(tempListing.RealtorId))
            {
                navigationManager.NavigateTo("/403-Forbidden");
                return;
            }
            listing = tempListing;
        }
    }
}
