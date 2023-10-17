using AutoMapper;
using Automobile.Entities.DbSet;
using Automobile.Entities.Dtos;
using Automobile.Entities.Dtos.Response;

namespace Automobile.Api.MappingProfiles.DomainToResponse;

public class DomainToResponse : Profile{
    public DomainToResponse()
    {
        CreateMap<Achievement, DriverAchievementResponse>()
            .ForMember(
            dest => dest.Wins,
            opt => opt.MapFrom(src => src.RaceWins));

        CreateMap<Driver, GetDriverResponse>()
            .ForMember(
            dest => dest.DriverId,
            opt => opt.MapFrom(src => src.Id));
    }
}