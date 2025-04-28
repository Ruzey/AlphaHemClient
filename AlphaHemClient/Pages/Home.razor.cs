using AlphaHemClient.Model.ViewModel;

namespace AlphaHemClient.Pages
{
    public partial class Home
    {
        private ListingPageViewModel viewModel = new ListingPageViewModel();
        private List<MunicipalityViewModel> municipalities = new List<MunicipalityViewModel>();
        private string? selectedMunicipality = null;
        private string? selectedSortOption = null;
        private int currentPage = 1;
        private int totalPages = 1;
        private int pageSize = 9;

        protected override async Task OnInitializedAsync()
        {
            municipalities = await MunicipalityService.GetMunicipalitiesAsync();

            await LoadListings(currentPage, pageSize);
        }

        private async Task OnMunicipalityChanged(string? municipality)
        {
            selectedMunicipality = municipality;
            currentPage = 1;
            await LoadListings(currentPage, pageSize);
        }

        private async Task OnSortChanged(string sortOption)
        {
            selectedSortOption = sortOption;
            currentPage = 1;
            await LoadListings(currentPage, pageSize);
        }

        private async Task OnPageChanged(int page)
        {
            currentPage = page;
            await LoadListings(currentPage, pageSize);
        }

        private async Task LoadListings(int pageIndex, int pageSize)
        {
            var response = await ListingService.GetPaginatedListings(pageIndex, pageSize, selectedMunicipality, selectedSortOption);

            viewModel.Listings = response.Listings;
            viewModel.TotalCount = response.TotalCount; ;
            totalPages = (int)Math.Ceiling((double)viewModel.TotalCount / pageSize);
            viewModel.CurrentPage = pageIndex;
        }
    }
}
