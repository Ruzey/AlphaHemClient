using System.ComponentModel.DataAnnotations;

namespace AlphaHemClient.Model.ViewModel
{
    //Author: ALL
    public class RealtorRegisterVM
    {
        [Required, EmailAddress(ErrorMessage = "Epost är obligatoriskt.")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$",
        ErrorMessage = "Lösenordet måste vara minst 6 tecken"+
                    "\nLösenordet måste innehålla minst ett specialtecken."+
                    "\nLösenordet måste innehålla minst en gemen ('a'-'z')." +
                    "\nLösenordet måste innehålla minst en versal ('A'-'Z').")]
        public string Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "Dina lösenord matchar inte")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
        public string LastName { get; set; }
        [Required, Phone(ErrorMessage = "Vänligen fyll i ett giltigt telefonnummer.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Mäklarbyrå är obligatoriskt.")]
        public int AgencyId { get; set; }
    }
}
