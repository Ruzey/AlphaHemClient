using System.ComponentModel.DataAnnotations;

namespace AlphaHemClient.Model.ViewModel
{
    // Author: Conny
    public class RealtorUpdateViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Epost är obligatoriskt.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefonnummer är obligatoriskt.")]
        [Phone]
        public string PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
