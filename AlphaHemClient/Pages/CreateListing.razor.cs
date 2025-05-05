using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AlphaHemClient.Pages
{
    //Author : Dominika
    // Co-author: Christoffer, Mattias, Conny
    public partial class CreateListing
    {
        private ListingCreateViewModel listing = new ListingCreateViewModel
        {
            Images = new List<string>(),
            MunicipalityId = 0
        };

        private string imageUrl { get; set; } = string.Empty;
        private List<MunicipalityViewModel> municipalities = new();

        [Inject] private HttpClient Http { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ListingService listingService { get; set; }
        [Inject] private MunicipalityService municipalityService { get; set; }
        [Inject] private ILocalStorageService localStorage { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await LoadMunicipalities();
            listing.RealtorId = await localStorage.GetItemAsync<string>("userId");
        }

        private async Task LoadMunicipalities()
        {
            municipalities = await municipalityService.GetMunicipalitiesAsync();
        }

        private string errorMessage;
        private async Task HandleValidSubmit()
        {
            try
            {
                await listingService.CreateListingAsync(listing);
                navigationManager.NavigateTo($"/realtor/{listing.RealtorId}");
            }
            catch (Exception ex)
            {
                errorMessage = "Kunde inte spara bostaden. Försök igen.";
                Console.WriteLine($"Fel: {ex.Message}");
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
    }

}
