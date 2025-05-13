using AlphaHemAPI.Data.DTO;
using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net;
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

        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ListingService listingService { get; set; }
        [Inject] private MunicipalityService municipalityService { get; set; }
        [Inject] private AuthService authService { get; set; }
        private string errorMessage;


        protected override async Task OnInitializedAsync()
        {
            await LoadMunicipalities();
            listing.RealtorId = await authService.GetLoggedInUserId();
        }

        private async Task LoadMunicipalities()
        {
            var response = await municipalityService.GetMunicipalitiesAsync();
            var page = NavHandler.Handler(response.StatusCode);
            if (page != null)
            {
                navigationManager.NavigateTo(page);
                return;
            }
            municipalities = response.Data;
        }
        private async Task HandleValidSubmit()
        {

            var response = await listingService.CreateListingAsync(listing);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                navigationManager.NavigateTo($"/realtor/{listing.RealtorId}");
                return;
            }
            errorMessage = $"Kunde inte spara bostaden : {response.Errors.FirstOrDefault()}.";
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
