using System.ComponentModel.DataAnnotations;

namespace AlphaHemAPI.Data.DTO
{
    public class RealtorLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
