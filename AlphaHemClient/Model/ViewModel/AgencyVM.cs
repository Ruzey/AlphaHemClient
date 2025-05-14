using AlphaHemAPI.Data.DTO;
using System.ComponentModel.DataAnnotations;

namespace AlphaHemClient.Model.ViewModel
{
    //Author: Mattias
    public class AgencyVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Presentation är obligatoriskt.")]
        public string Presentation { get; set; }
        public string Logo { get; set; }

        public List<RealtorDto> Realtors { get; set; }
    }
}
