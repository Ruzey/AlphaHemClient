namespace AlphaHemClient.Model.DTO
{
    // Author : Smilla
    public class ListingListDto
    {
        public int Id { get; set; }
        public string FirstImage { get; set; } // Author: Christoffer
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Municipality { get; set; }
        public string RealtorFullName { get; set; }
    }
}
