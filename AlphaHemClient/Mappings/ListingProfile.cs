using AutoMapper;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;
using AlphaHemAPI.Data.DTO;

namespace AlphaHemClient.Mappings
{
    //Author: Christoffer
    public class ListingProfile : Profile
    {
        public ListingProfile()
        {
            CreateMap<PagedListingListDto, ListingPageViewModel>();
            CreateMap<ListingListDto, MyListingViewModel>();
            CreateMap<ListingCreateViewModel, ListingCreateDto>();
            CreateMap<ListingUpdateViewModel, ListingUpdateDto>();
            CreateMap<ListingDetailsDto, ListingDetailsViewModel>();
            CreateMap<ListingDetailsDto, ListingUpdateViewModel>()
                .ForMember(dest => dest.RealtorId, from => from.MapFrom(src => src.Realtor.Id));
        }
    }
}
