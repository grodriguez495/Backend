using AirQualityControlAPI.Application.Features.Users;
using AirQualityControlAPI.Domain.Models;
using AutoMapper;

namespace AirQualityControlAPI.Infrastructure.Persistence.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId))
         // .ForMember(dest => dest.RoleId, options => options.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, options => options.MapFrom(src => src.Password))
            .ForMember(dest => dest.Phone, options => options.MapFrom(src => src.Phone))
            .ForMember(dest => dest.IsActive, options => options.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.RoleName, options =>  options.MapFrom(src => src.Role.Name));
    }
}
