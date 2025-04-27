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

        private string FormatCurrency(decimal value)
        {
            if (value == 0)
            {
                return "Ingen avgift";
            }
            return value.ToString("N0").Replace(",", " ") + " kr";
        }
    }
}
