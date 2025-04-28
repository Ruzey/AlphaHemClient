using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Services;

namespace AlphaHemClient.Pages
{
    //Author : Smilla
    public partial class RealtorProfilePage : ComponentBase
    {
        private RealtorProfileViewModel? realtorProfile;
        private bool isLoading = false;
        private bool isError = false;

        [Parameter]
        public int Id { get; set; }

        [Inject]
        private RealtorService RealtorService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            isError = false;
            try
            {
                realtorProfile = await RealtorService.GetRealtorByIdAsync(Id);
                if (realtorProfile == null)
                {
                    isError = true;
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
