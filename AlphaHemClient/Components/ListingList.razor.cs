using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;


namespace AlphaHemClient.Components
{
    public partial class ListingList : ComponentBase
    {
        private object _viewModel;

        [Inject]
        private ListingService ListingService { get; set; }

        protected override async Task OnInitializedAsync()
        { 
            _viewModel = await ListingService.GetPaginatedListingsAsync();
        }

    }
}
