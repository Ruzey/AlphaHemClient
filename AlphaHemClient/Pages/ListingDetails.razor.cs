using AlphaHemAPI.Data.DTO;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Json;
namespace AlphaHemClient.Pages
//Author : Niklas
{
    public partial class ListingDetails
    {
        [Parameter]
        public int id { get; set; }

        private ListingDetailsDto? listing;

        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            listing = await Http.GetFromJsonAsync<ListingDetailsDto>($"/api/listing/{id}");
        }
    }
}
