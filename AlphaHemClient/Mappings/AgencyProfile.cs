using AutoMapper;
using AlphaHemAPI.Data.DTO;
using AlphaHemClient.Model.ViewModel;

public class AgencyProfile : Profile
{
    //Author: Mattias
    public AgencyProfile()
    {
        CreateMap<AgencyWithRealtorsDto, AgencyNamesViewModel>();
    }
}