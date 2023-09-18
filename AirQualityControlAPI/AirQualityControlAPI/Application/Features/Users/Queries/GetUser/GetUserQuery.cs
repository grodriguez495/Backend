using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Queries.GetUser;

public class GetUserQuery : IRequest<UserDto>
{
    public int UserId { get; set; }

    public GetUserQuery(int userId)
    {
        UserId = userId;
    }
}