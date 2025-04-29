using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;
using System.Net.Http.Json;

namespace AlphaHemClient.Services
{
    // Author : Smilla
    public class RealtorService
    {
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;

        public RealtorService(HttpClient httpClient, IMapper mapper)
        {
            this.httpClient = httpClient;
            this.mapper = mapper;
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
                    FullName = $"{response.FirstName} {response.LastName}",
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

        // Author: Conny
        public async Task<RealtorEditViewModel> GetRealtorForEditAsync(int id)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<RealtorDto>($"api/realtor/{id}");
                if (response == null)
                    return null;

                var editRealtorVM = mapper.Map<RealtorEditViewModel>(response);
                return editRealtorVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching realtor with ID {id}: {ex.Message}");
                return null;
            }
        }

        // Author: Conny
        public async Task<bool> UpdateRealtorAsync(RealtorEditViewModel realtorVM)
        {
            var realtorDto = mapper.Map<RealtorUpdateDto>(realtorVM);

            try
            {
                var response = await httpClient.PutAsJsonAsync($"api/realtor/{realtorDto.Id}", realtorDto);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Fel vid uppdatering av mäklare. Status: {(int)response.StatusCode} {response.ReasonPhrase}. Svar: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett oväntat fel uppstod vid uppdatering av mäklare: {ex.Message}");
                return false;
            }
        }
    }
}