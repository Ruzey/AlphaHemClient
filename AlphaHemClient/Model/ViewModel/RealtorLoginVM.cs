using System.ComponentModel.DataAnnotations;

namespace AlphaHemClient.Model.ViewModel
{
    public class RealtorLoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
