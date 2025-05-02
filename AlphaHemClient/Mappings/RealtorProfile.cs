using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;
using AutoMapper;

namespace AlphaHemClient.Mappings
{
    // Author: Conny
    public class RealtorProfile : Profile
    {
        public RealtorProfile()
        {
            CreateMap<RealtorDto, RealtorEditViewModel>();
            CreateMap<RealtorEditViewModel, RealtorUpdateDto>();
            CreateMap<RealtorRegisterVM, RealtorRegisterDto>();
        }
    }
}
