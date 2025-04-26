using AlphaHemClient.Model.DTO;
using AutoMapper;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using AlphaHemClient.Model.ViewModel;

namespace AlphaHemClient.Services
{
    // Author: Christoffer
    public class MunicipalityService
    {
        private readonly HttpClient _http;
        private readonly IMapper _mapper;

        public MunicipalityService(HttpClient http, IMapper mapper)
        {
            _http = http;
            _mapper = mapper;
        }

        public async Task<List<MunicipalityViewModel>> GetMunicipalitiesAsync()
        {
            try
            {
                var dtoList = await _http.GetFromJsonAsync<List<MunicipalityListDto>>("api/Municipality");

                if (dtoList == null)
                    return new List<MunicipalityViewModel>();

                var vmList = _mapper.Map<List<MunicipalityViewModel>>(dtoList);

                return vmList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching municipalities: " + ex.Message);
                return new List<MunicipalityViewModel>();
            }

        }
    }
}
