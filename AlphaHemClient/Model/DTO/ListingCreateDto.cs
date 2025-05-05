using System.ComponentModel.DataAnnotations;
using AlphaHemAPI.Data.Models;

namespace AlphaHemAPI.Data.DTO
{
    // Author: Conny
    // Co-Author: Dominika, Mattias, Christoffer
    public class ListingCreateDto
    {

        public int? Rooms { get; set; }
        public int? YearBuilt { get; set; }
        public decimal? Price { get; set; }
        public decimal? MonthlyFee { get; set; }
        public decimal? YearlyOperatingCost { get; set; }
        public decimal? LivingArea { get; set; }
        public decimal? SecondaryArea { get; set; }
        public decimal? LotArea { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }


        // Enum
        public Category Category { get; set; }


        // Relations. Entities are fetched from these IDs to minimize data transfer
        public int MunicipalityId { get; set; } = 0;
        public string RealtorId { get; set; }
    }
}
