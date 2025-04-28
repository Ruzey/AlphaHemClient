using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaHemClient.Pages
{
    //Author: Dominika
    public partial class EditListing
    {
        [Parameter] public string Id { get; set; }
        private ListingUpdateDto listing = new ListingUpdateDto { Images = new List<string>() };
        private string imageUrl { get; set; } = string.Empty;
        private string errorMessage;
        [Inject] private ListingService listingService { get; set; }
        [Inject] private HttpClient Http { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }



        private async Task LoadListing()
        {
            try
            {
                if(string.IsNullOrEmpty(Id))
                {
                    errorMessage = "Ingen giltig ID angiven.";
                    return;
                }

                var response = await listingService.GetListingByIdAsync(Id);
                if (response != null)
                {
                    listing = response;
                }
                else
                {
                    errorMessage = "Bostadsobjektet kunde inte hämtas.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Fel vid hämtning av objekt: {ex.Message}";
            }
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

        private async Task HandleValidSubmit()
        {
            try
            {
                listing.RealtorId = 1; //Tillfälligt tills att vi fixar identity

                await listingService.UpdateListingAsync(Id, listing);
                await Task.Delay(1000);

                navigationManager.NavigateTo($"/listings/{Id}");
            }
            catch (Exception ex)
            {
                errorMessage = $"Fel vid uppdatering av bostad: {ex.Message}";
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadListing();
        }
    }
}
