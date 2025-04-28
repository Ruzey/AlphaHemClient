using AlphaHemAPI.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace AlphaHemAPI.Data.DTO
{
    // Author: Conny
    public class ListingUpdateDto
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal MonthlyFee { get; set; }
        [Required]
        public decimal YearlyOperatingCost { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<string> Images { get; set; }


        // Relations. Entities are fetched from these IDs to minimize data transfer
        [Required]
        public int RealtorId { get; set; }
    }
}
