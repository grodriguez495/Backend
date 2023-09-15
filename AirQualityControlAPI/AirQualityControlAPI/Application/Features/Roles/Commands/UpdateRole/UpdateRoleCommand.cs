using System.Text.Json.Serialization;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<RoleDto>
{
    public int RoleId { get; set; }
    public string Name { get; set; }

    [JsonConstructor]
    public UpdateRoleCommand()
    {
    }
}