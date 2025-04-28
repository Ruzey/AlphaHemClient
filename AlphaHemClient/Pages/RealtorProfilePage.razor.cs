using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;

namespace AlphaHemClient.Pages
{
    public partial class RealtorProfilePage : ComponentBase
    {
        private int enteredId;
        private RealtorProfileViewModel? realtorProfile;
        private bool isLoading = false;
        private bool isError = false;

        [Inject]
        private RealtorService RealtorService { get; set; }  // Inject RealtorService

        private async Task FetchRealtorProfile()
        {
            isLoading = true;
            isError = false;
            try
            {
                realtorProfile = await RealtorService.GetRealtorByIdAsync(enteredId);
                if (realtorProfile == null)
                {
                    isError = true; // Handle case where realtor is not found
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching realtor: {ex.Message}");
                isError = true;
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
