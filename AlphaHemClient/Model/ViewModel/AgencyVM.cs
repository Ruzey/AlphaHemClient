using AlphaHemAPI.Data.DTO;

namespace AlphaHemClient.Model.ViewModel
{
    //Author: Mattias
    public class AgencyVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public string Logo { get; set; }

        public List<RealtorDto> Realtors { get; set; }
    }
}
