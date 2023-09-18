using MediatR;

namespace AirQualityControlAPI.Application.Features.Users.Commands.DeleteUsers;

public class DeleteUserCommand : IRequest<bool>
{
    public int Id { get; set; }
}