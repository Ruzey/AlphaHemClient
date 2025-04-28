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

        public async Task<List<AgencyNamesViewModel>> GetAllAgencyNames()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<List<AgencyNamesViewModel>>("api/agency");

                return response ?? new List<AgencyNamesViewModel>();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching agencies", ex);
            }
        }

        public async Task<List<AgencyVM>> GetAllAgencies()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<List<AgencyVM>>("api/agency");
                return response ?? new List<AgencyVM>();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching agencies", ex);
            }
        }

        public async Task<AgencyVM> GetAgencyById(int id)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<AgencyVM>($"api/agency/{id}");
                return response ?? new AgencyVM();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the agency", ex);
            }
        }
    }
}
