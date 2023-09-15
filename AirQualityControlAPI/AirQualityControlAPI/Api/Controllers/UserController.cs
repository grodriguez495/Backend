using MediatR;

namespace AirQualityControlAPI.Api.Controllers;

public class UserController : BaseController
{
    public UserController(IMediator sender) : base(sender)
    {
    }
}