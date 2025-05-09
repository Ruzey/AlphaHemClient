namespace AlphaHemClient.Model.DTO
{
    // Author: Christoffer
    public class PagedListingListDto
    {
        public int TotalCount { get; set; }
        public List<ListingListDto> Listings { get; set;  } = new List<ListingListDto>();
    }
}
