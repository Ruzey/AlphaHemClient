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
        private readonly JsLoggingService _jsLoggingService;

        public MunicipalityService(HttpClient http, IMapper mapper, JsLoggingService jsLoggingService)
        {
            _http = http;
            _mapper = mapper;
            _jsLoggingService = jsLoggingService;
        }

        public async Task<List<MunicipalityViewModel>> GetMunicipalitiesAsync()
        {
            try
            {
                var dtoList = await _http.GetFromJsonAsync<List<MunicipalityListDto>>("api/Municipality");

                if (dtoList == null)
                {
                    await _jsLoggingService.LogToConsole($"Error fetching municipalities");
                    return new List<MunicipalityViewModel>();
                }

                var vmList = _mapper.Map<List<MunicipalityViewModel>>(dtoList);

                return vmList;
            }
            catch (Exception ex)
            {
                await _jsLoggingService.LogToConsole($"Error fetching municipalities: {ex.Message}");
                return new List<MunicipalityViewModel>();
            }

        }
    }
}
