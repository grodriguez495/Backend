using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Domain.Models;
using AutoMapper;

namespace AirQualityControlAPI.Infrastructure.Persistence.Mapping
{
    public class RoleProfile: Profile
    {
        public RoleProfile() 
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name));
        }
    }
}
