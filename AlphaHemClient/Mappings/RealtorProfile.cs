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
            CreateMap<RealtorDto, RealtorUpdateViewModel>();
            CreateMap<RealtorUpdateViewModel, RealtorUpdateDto>();
            CreateMap<RealtorRegisterVM, RealtorRegisterDto>();
            CreateMap<RealtorLoginVM, RealtorLoginDto>();
            CreateMap<RealtorDto, RealtorProfileViewModel>()
                .ForMember(dest => dest.FullName, from => from.MapFrom(src => $"{src.FirstName} {src.LastName}"));

                
        }
    }
}
