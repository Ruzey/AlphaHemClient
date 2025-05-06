using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Blazored.LocalStorage;
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
        private ListingUpdateViewModel listing = new ListingUpdateViewModel { Images = new List<string>() };
        private string imageUrl { get; set; } = string.Empty;
        private string errorMessage;
        private string currentUser;
        [Inject] private ListingService listingService { get; set; }
        [Inject] private HttpClient Http { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ILocalStorageService localStorage { get; set; }



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

        // Co-author: ALL
        private async Task HandleValidSubmit()
        {
            try
            {
                var idToInt = Convert.ToInt32(Id);
                await listingService.UpdateListingAsync(idToInt, listing);
                await Task.Delay(1000);

                navigationManager.NavigateTo($"/listings/{Id}");
            }
            catch (Exception ex)
            {
                errorMessage = $"Fel vid uppdatering av bostad: {ex.Message}";
            }
        }

        // Co-author: ALL
        protected override async Task OnInitializedAsync()
        {
            currentUser = await localStorage.GetItemAsync<string>("userId");
            await LoadListing();
            if (!string.Equals(currentUser, listing.RealtorId))
            {
                navigationManager.NavigateTo("/login");
            }
        }

    }
}
