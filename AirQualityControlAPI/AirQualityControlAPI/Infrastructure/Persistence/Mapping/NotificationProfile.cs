using AirQualityControlAPI.Application.Features.Notifications;
using AirQualityControlAPI.Domain.Models;
using AutoMapper;

namespace AirQualityControlAPI.Infrastructure.Persistence.Mapping;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<AlertNotification, NotificationDto>();
        
        //    .ForMember(dest => dest.Timestamp,options => options.MapFrom(src =>src.Timestamp.ToString()));
        /*    .ForMember(dest => dest.AlertId, options => options.MapFrom(src => src.AlertId))
            .ForMember(dest => dest.Timestamp,options => options.MapFrom(src =>src.Timestamp.ToString()))
            .ForMember(dest => dest.Message,options => options.MapFrom(src =>src.Message))
            .ForMember(dest => dest.Recipient,options => options.MapFrom(src =>src.Recipient))
            .ForMember(dest => dest.AlertType,options => options.MapFrom(src =>src.AlertType));*/
    }
}