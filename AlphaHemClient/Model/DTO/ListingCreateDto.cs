using System.ComponentModel.DataAnnotations;
using AlphaHemAPI.Data.Models;

namespace AlphaHemAPI.Data.DTO
{
    // Author: Conny
    // Co-Author: Dominika
    public class ListingCreateDto
    {
        [Required(ErrorMessage = "Antal rum är obligatoriskt.")]
        public int? Rooms { get; set; }
        [Required(ErrorMessage = "Byggår är obligatoriskt.")]
        public int? YearBuilt { get; set; }
        [Required(ErrorMessage = "Pris är obligatoriskt.")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Månadsavgift är obligatoriskt.")]
        public decimal? MonthlyFee { get; set; }
        [Required(ErrorMessage = "Årsavgift är obligatoriskt.")]
        public decimal? YearlyOperatingCost { get; set; }
        [Required(ErrorMessage = "Boarea är obligatoriskt.")]
        public decimal? LivingArea { get; set; }
        [Required(ErrorMessage = "Biarea är obligatoriskt.")]
        public decimal? SecondaryArea { get; set; }
        [Required(ErrorMessage = "Tomtarea är obligatoriskt.")]
        public decimal? LotArea { get; set; }
        [Required(ErrorMessage = "Adress är obligatoriskt.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Beskrivning är obligatoriskt.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Bilder är obligatoriskt.")]
        public List<string> Images { get; set; }

        // Enum
        [Required(ErrorMessage = "Kategori är obligatoriskt.")]
        public Category Category { get; set; }

        // Relations. Entities are fetched from these IDs to minimize data transfer
        [Required(ErrorMessage = "Kommun är obligatoriskt.")]
        public int MunicipalityId { get; set; } = 0;
        [Required]
        public int RealtorId { get; set; }
    }
}
