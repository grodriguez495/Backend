using System.Text.Json.Serialization;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Commands.UpdateUsers;

public class UpdateUserCommand: IRequest<UserDto>
{
    public int UserId { get; set; }
    public string Name { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
    public int RoleId { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; }

    [JsonConstructor]
    public UpdateUserCommand()
    {
    }
}