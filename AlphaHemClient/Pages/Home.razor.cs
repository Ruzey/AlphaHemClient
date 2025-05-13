using AlphaHemClient.HelperClasses;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;
using Microsoft.AspNetCore.Components;

namespace AlphaHemClient.Pages
{
    public partial class Home
    {
        [Inject] private MunicipalityService municipalityService { get; set; }
        [Inject] private ListingService listingService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
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
            
            var response = await municipalityService.GetMunicipalitiesAsync();
            municipalities = response.Data;

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
            var response = await listingService.GetPaginatedListingsAsync(
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
