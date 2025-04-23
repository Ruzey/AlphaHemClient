using System.ComponentModel.DataAnnotations;
using AlphaHemAPI.Data.Models;

namespace AlphaHemAPI.Data.DTO
{
    // Author: Conny
    public class ListingCreateDto
    {
        [Required]
        public int Rooms { get; set; }
        [Required]
        public int YearBuilt { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal MonthlyFee { get; set; }
        [Required]
        public decimal YearlyOperatingCost { get; set; }
        [Required]
        public decimal LivingArea { get; set; }
        [Required]
        public decimal SecondaryArea { get; set; }
        [Required]
        public decimal LotArea { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<string> Images { get; set; }

        // Enum
        [Required]
        public Category Category { get; set; }

        // Relations. Entities are fetched from these IDs to minimize data transfer
        [Required]
        public int MunicipalityId { get; set; }
        [Required]
        public int RealtorId { get; set; }
    }
}
