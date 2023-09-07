using MediatR;
using System.Text.Json.Serialization;

namespace AirQualityControlAPI.Application.Features.Roles
{
    public class GetRoleQuery : IRequest<List<RoleDto>>
    {
        [JsonConstructor]
        public GetRoleQuery() { }
    }
}
