using AlphaHemAPI.Data.DTO;

namespace AlphaHemClient.Model.ViewModel
{
    public class ListingUpdateViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal YearlyOperatingCost { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public string RealtorId { get; set; }
    }
}