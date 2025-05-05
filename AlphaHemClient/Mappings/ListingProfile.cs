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
        }
    }
}
