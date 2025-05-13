using System.ComponentModel.DataAnnotations;

namespace AlphaHemClient.Model.ViewModel
{
    // Author: Conny
    public class RealtorUpdateViewModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
