using AirQualityControlAPI.Application.Features.Logins;
using AirQualityControlAPI.Application.Features.Logins.Query.LogIns;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirQualityControlAPI.Api.Controllers;

public class LoginController : BaseController
{
    public LoginController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<LoginDto?>> LoginAsync(string email, string password)
    {
        try
        {
            return await _mediator.Send(new LoginQuery(email,password));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}