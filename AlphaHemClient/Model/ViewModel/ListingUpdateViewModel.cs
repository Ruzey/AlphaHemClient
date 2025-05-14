using AlphaHemAPI.Data.DTO;
using System.ComponentModel.DataAnnotations;

namespace AlphaHemClient.Model.ViewModel
{
    public class ListingUpdateViewModel
    {
        public int Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Priset får inte vara mindre än 0.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Månadsavgiften får inte vara mindre än 0.")]
        public decimal MonthlyFee { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Årsavgiften får inte vara mindre än 0.")]
        public decimal YearlyOperatingCost { get; set; }

        [MinLength(5, ErrorMessage = "Beskrivningen måste vara minst 5 tecken lång.")]
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public string RealtorId { get; set; }
    }
}