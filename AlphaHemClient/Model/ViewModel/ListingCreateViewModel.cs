using System.ComponentModel.DataAnnotations;
using AlphaHemAPI.Data.Models;

namespace AlphaHemClient.Model.ViewModel
{
    // Author: Conny, Mattias, Christoffer
    public class ListingCreateViewModel
    {
        [Required(ErrorMessage = "Antal rum är obligatoriskt.")]
        [Range(0, int.MaxValue, ErrorMessage = "Antal rum får inte vara mindre än 0.")]
        public int? Rooms { get; set; }

        [Required(ErrorMessage = "Byggår är obligatoriskt.")]
        [Range(0, int.MaxValue, ErrorMessage = "Byggår får inte vara mindre än 0.")]
        public int? YearBuilt { get; set; }

        [Required(ErrorMessage = "Pris är obligatoriskt.")]
        [Range(0, int.MaxValue, ErrorMessage = "Priset får inte vara mindre än 0.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Månadsavgift är obligatoriskt.")]
        [Range(0, int.MaxValue, ErrorMessage = "Månadsavgiften får inte vara mindre än 0.")]
        public decimal? MonthlyFee { get; set; }

        [Required(ErrorMessage = "Årsavgift är obligatoriskt.")]
        [Range(0, int.MaxValue, ErrorMessage = "Årsavgiften får inte vara mindre än 0.")]
        public decimal? YearlyOperatingCost { get; set; }

        [Required(ErrorMessage = "Boarea är obligatoriskt.")]
        [Range(0, int.MaxValue, ErrorMessage = "Boarea får inte vara mindre än 0.")]
        public decimal? LivingArea { get; set; }

        [Required(ErrorMessage = "Biarea är obligatoriskt.")]
        [Range(0, int.MaxValue, ErrorMessage = "Biarea får inte vara mindre än 0.")]
        public decimal? SecondaryArea { get; set; }

        [Required(ErrorMessage = "Tomtarea är obligatoriskt.")]
        [Range(0, int.MaxValue, ErrorMessage = "Tomtarea får inte vara mindre än 0.")]
        public decimal? LotArea { get; set; }

        [Required(ErrorMessage = "Adress är obligatoriskt.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatoriskt.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bilder är obligatoriskt.")]
        [Range(1, 40, ErrorMessage = "Du måste ladda upp minst 1 bild och max 40 bilder.")]
        public List<string> Images { get; set; }


        // Enum
        [Required(ErrorMessage = "Kategori är obligatoriskt.")]
        [Range(1, int.MaxValue, ErrorMessage = "Du måste välja en kategori.")]
        public Category Category { get; set; }


        // Relations
        [Required(ErrorMessage = "Kommun är obligatoriskt.")]
        [Range(1, int.MaxValue, ErrorMessage = "Du måste välja en kommun.")]
        public int MunicipalityId { get; set; } = 0;

        [Required]
        public string RealtorId { get; set; }
    }
}
