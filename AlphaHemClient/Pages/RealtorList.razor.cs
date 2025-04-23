using AlphaHemAPI.Data.DTO;
using System.Net.Http.Json;

namespace AlphaHemClient.Pages
{
    /* Detta är bara test-kod för att testa så att man får API kopplingen att fungera, bör raderas senare innan inlämning.*/

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
