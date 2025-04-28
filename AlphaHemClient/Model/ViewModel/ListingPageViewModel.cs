using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ComponentModels;

namespace AlphaHemClient.Model.ViewModel
{
    // Author: Christoffer
    public class ListingPageViewModel
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<ListingListDto> Listings { get; set; } = new List<ListingListDto>();


        public string? Municipality { get; set; }
        public string? SortBy { get; set; }

        public List<SortOption> SortOptions { get; set; } = new List<SortOption>
        {
            new SortOption { Value = "price", Label = "Pris (Lägst först)" },
            new SortOption { Value = "price_desc", Label = "Pris (Högst först)" },
            new SortOption { Value = "category", Label = "Kategori (A-Ö)" },
            new SortOption { Value = "category_desc", Label = "Kategori (Ö-A)" },
        };

    }
}
