using System.Text.Json.Serialization;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Queries.GetUsers;

public class GetUsersQuery: IRequest<List<UserDto>>
{
    [JsonConstructor]
    public GetUsersQuery()
    {
    }
}