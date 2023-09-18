using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Commands.CreateUsers;

public class CreateUserCommand : IRequest<bool>
{
    public string Name { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
    public int RoleId { get; set; }
    public string Phone { get; set; }

}