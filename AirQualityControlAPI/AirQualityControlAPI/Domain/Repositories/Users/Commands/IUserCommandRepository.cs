using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Domain.Repositories.Users.Commands;

public interface IUserCommandRepository:
    ICreateCommandRepository<User,int>,
    IUpdateCommandRepository<User,int>
{
    
}