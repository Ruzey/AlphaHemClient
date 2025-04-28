using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AlphaHemClient.Pages
{
    //Author : Dominika
    public partial class CreateListing
    {
        private ListingCreateDto listing = new ListingCreateDto
        {
            Images = new List<string>(),
            MunicipalityId = 0
        };

        private string imageUrl {  get; set; } = string.Empty;
        private List<MunicipalityListDto> municipalities = new();

        [Inject] private HttpClient Http {  get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ListingService listingService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await LoadMunicipalities();
        }

        private async Task LoadMunicipalities()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<List<MunicipalityListDto>>("/api/municipality");
                if (response != null)
                {
                    municipalities = response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid hämtning av kommuner: {ex.Message}");
            }
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                listing.RealtorId = 1; //Detta är bara exempel och byts ut när vi fixar identity

                await listingService.CreateListingAsync(listing);
                navigationManager.NavigateTo("/minaobjekt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel uppstod vid skapandet av bostad: {ex.Message}");
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
