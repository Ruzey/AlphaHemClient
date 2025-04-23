using AlphaHemAPI.Data.Models;

namespace AlphaHemAPI.Data.DTO
{
    // Author : Smilla
    public class ListingDetailsDto
    {
            public int Id { get; set; }
            public string Address { get; set; }
            public int Rooms { get; set; }
            public int YearBuilt { get; set; }
            public decimal Price { get; set; }
            public decimal MonthlyFee { get; set; }
            public decimal YearlyOperatingCost { get; set; }
            public decimal LivingArea { get; set; }
            public decimal SecondaryArea { get; set; }
            public decimal LotArea { get; set; }
            public string Description { get; set; }
            public List<string> Images { get; set; }

            public string Category { get; set; }

            public string MunicipalityName { get; set; }
            public RealtorInListingDto Realtor { get; set; }
    }
}
