using System.ComponentModel.DataAnnotations;

namespace AlphaHemAPI.Data.DTO
{
    //Author: Christoffer
    public class RealtorUpdateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string ProfilePicture { get; set; }
    }
}
