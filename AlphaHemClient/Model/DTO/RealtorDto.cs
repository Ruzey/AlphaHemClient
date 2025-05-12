namespace AlphaHemAPI.Data.DTO
{
    public class RealtorDto
    {
        //Author: Mattias
        //Co-author: Dominika, Conny
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public string AgencyName { get; set; }
        public int AgencyId { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
