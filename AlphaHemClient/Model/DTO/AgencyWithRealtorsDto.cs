namespace AlphaHemAPI.Data.DTO
{
    public class AgencyWithRealtorsDto
    {
        //Author: Mattias
        public int Id { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public string Logo { get; set; }

        public List<RealtorDto> Realtors { get; set; }
    }
}
