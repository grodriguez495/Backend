using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Commands.CreateRoles;

public class CreateRoleCommand : IRequest<bool>
{
    public string Name { get; set; }
}