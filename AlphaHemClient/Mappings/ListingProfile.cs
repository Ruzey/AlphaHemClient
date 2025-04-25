using AutoMapper;
using AlphaHemClient.Model.DTO;
using AlphaHemClient.Model.ViewModel;

namespace AlphaHemClient.Mappings
{
    // Author: Christoffer
    public class ListingProfile : Profile
    {
        public ListingProfile()
        {
            CreateMap<PagedListingListDto, ListingPageViewModel>();
        }
    }
}
