using System.Text.Json.Serialization;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<RoleDto>>
    {
        [JsonConstructor]
        public GetRolesQuery() { }
    }
}
