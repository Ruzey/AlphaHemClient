using System.ComponentModel.DataAnnotations;

namespace AlphaHemClient.Model.ViewModel
{
    // Author: Conny
    public class MyListingViewModel
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string? Municipality { get; set; }
    }
}
