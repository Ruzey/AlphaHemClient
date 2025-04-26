using AlphaHemClient.Model.ViewModel;
using AlphaHemClient.Model.DTO;
using AutoMapper;

namespace AlphaHemClient.Mappings
{
    // Author: Christoffer
    public class MunicipalityProfile : Profile
    {
        public MunicipalityProfile()
        { 
            CreateMap<MunicipalityListDto, MunicipalityViewModel>();
        }
    }
}
