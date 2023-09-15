using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Domain.Repositories.Roles.Commands;

public interface IRoleCommandRepository:
    ICreateCommandRepository<Role,int>,
    IUpdateCommandRepository<Role,int>
{
    
}