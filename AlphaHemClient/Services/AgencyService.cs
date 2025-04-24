using System.Net.Http.Json;
using AlphaHemClient.Model.ViewModel;

namespace AlphaHemClient.Services
{
    //Author: Mattias
    public class AgencyService
    {
        private readonly HttpClient _http;

        public AgencyService(HttpClient http)
        {
            this._http = http;
        }

        public async Task<List<AgencNamesViewModel>> GetAllAgencyNames()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<List<AgencNamesViewModel>>("api/agency");

                return response ?? new List<AgencNamesViewModel>();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching agencies", ex);
            }
        }
    }
}
