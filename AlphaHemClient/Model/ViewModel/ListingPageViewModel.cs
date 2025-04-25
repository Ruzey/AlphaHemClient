using AlphaHemAPI.Data.DTO;

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

        public List<String> SortOptions { get; set; } = new List<String>();
        {
            "price_asc", "price_desc", "category_asc", "category_desc"
        };

    }
}
