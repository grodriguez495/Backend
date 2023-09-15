using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Commands.DeleteRoles;

public class DeleteRoleCommand : IRequest<bool>
{
    public int Id { get; set; }
}