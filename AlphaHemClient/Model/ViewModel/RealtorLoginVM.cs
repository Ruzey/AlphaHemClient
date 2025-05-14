using System.ComponentModel.DataAnnotations;

namespace AlphaHemClient.Model.ViewModel
{
    public class RealtorLoginVM
    {
        [Required(ErrorMessage = "Epost är obligatorisk.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lösenord är obligatoriskt.")]
        public string Password { get; set; }
    }
}
