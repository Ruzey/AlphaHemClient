using AlphaHemAPI.Data.DTO;
using System.Net.Http.Json;

namespace AlphaHemClient.Pages
{
    public partial class RealtorList
    {
        private List<RealtorDto> realtors;

        protected override async Task OnInitializedAsync()
        {
            var response = await Http.GetFromJsonAsync<List<RealtorDto>>("https://localhost:7109/api/Realtor");
            if (response != null)
            {
                realtors = response;
            }
            else
            {
                realtors = new List<RealtorDto>();
            }
        }
    }
}
