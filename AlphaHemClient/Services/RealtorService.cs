using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using System.Net.Http.Json;

namespace AlphaHemClient.Services
{
    // Author : Smilla
    public class RealtorService
    {
        private readonly HttpClient httpClient;

        public RealtorService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<RealtorProfileViewModel> GetRealtorByIdAsync(int id)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<RealtorDto>($"api/realtor/{id}");
                if (response == null)
                {
                    return null;
                }
                var profileViewModel = new RealtorProfileViewModel
                {
                    Id = response.Id,
                    FullName = response.FullName,
                    PhoneNumber = response.PhoneNumber,
                    Email = response.Email,
                    AgencyName = response.Agency,
                    ProfilePicture = response.ProfilePicture
                };
                return profileViewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching realtor with ID {id}: {ex.Message}");
                return null;
            }
        }
    }
}