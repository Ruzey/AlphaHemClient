using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;

namespace AlphaHemClient.Pages
{
    public partial class Home
    {
        private ListingPageViewModel viewModel = new ListingPageViewModel();
        private List<MunicipalityViewModel> municipalities = new List<MunicipalityViewModel>();
        private string? selectedMunicipality = null;
        private string? selectedCategory = null;
        private string? selectedSortOption = null;
        private int currentPage = 1;
        private int totalPages = 1;
        private int pageSize = 9;

        protected override async Task OnInitializedAsync()
        {
            municipalities = await MunicipalityService.GetMunicipalitiesAsync();

            await LoadListingsAsync(currentPage, pageSize);
        }

        private async Task OnMunicipalityChanged(string? municipality)
        {
            selectedMunicipality = municipality;
            currentPage = 1;
            await LoadListingsAsync(currentPage, pageSize);
        }
        private async Task OnCategoryChanged(string? category)
        {
            selectedCategory = category;
            currentPage = 1;
            await LoadListingsAsync(currentPage, pageSize);
        }

        private async Task OnSortChanged(string sortOption)
        {
            selectedSortOption = sortOption;
            currentPage = 1;
            await LoadListingsAsync(currentPage, pageSize);
        }

        private async Task OnPageChanged(int page)
        {
            currentPage = page;
            await LoadListingsAsync(currentPage, pageSize);
            await InvokeAsync(StateHasChanged);
        }

        private async Task LoadListingsAsync(int pageIndex, int pageSize)
        {
            var response = await ListingService.GetPaginatedListingsAsync(
                pageIndex,
                pageSize,
                selectedMunicipality,
                selectedCategory,
                selectedSortOption);

            viewModel.Listings = response.Listings;
            viewModel.TotalCount = response.TotalCount;
            totalPages = (int)Math.Ceiling((double)viewModel.TotalCount / pageSize);
        }
    }
}
