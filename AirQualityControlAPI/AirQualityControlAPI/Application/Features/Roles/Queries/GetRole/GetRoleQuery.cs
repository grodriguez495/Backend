using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Queries.GetRole;

public class GetRoleQuery : IRequest<RoleDto>
{
    public int RoleId { get; set; }

    public GetRoleQuery(int roleId)
    {
        RoleId = roleId;
    }
}