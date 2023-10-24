using AirQualityControlAPI.Application.Features.Logins;
using AirQualityControlAPI.Application.Features.Logins.Commands.LogIns;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirQualityControlAPI.Api.Controllers;

public class LoginController : BaseController
{
    public LoginController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpPost]
    public async Task<ActionResult<LoginDto?>> LoginAsync(LoginCommand command)
    {
        try
        {
            return await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}