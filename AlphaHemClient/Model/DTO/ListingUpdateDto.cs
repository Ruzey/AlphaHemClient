using AlphaHemAPI.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace AlphaHemAPI.Data.DTO
{
    // Author: Conny
    // Co-Author: Dominika
    public class ListingUpdateDto
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Priset får inte vara mindre än 0.")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Månadsavgiften får inte vara mindre än 0.")]
        public decimal MonthlyFee { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Årsavgiften får inte vara mindre än 0.")]
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
