using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Application.Features.Roles.Commands.DeleteRoles;
using AirQualityControlAPI.Application.Features.Roles.Queries.GetRoles;
using AirQualityControlAPI.Application.Features.Users;
using AirQualityControlAPI.Application.Features.Users.Commands.CreateUsers;
using AirQualityControlAPI.Application.Features.Users.Commands.DeleteUsers;
using AirQualityControlAPI.Application.Features.Users.Commands.UpdateUsers;
using AirQualityControlAPI.Application.Features.Users.Queries.GetUser;
using AirQualityControlAPI.Application.Features.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirQualityControlAPI.Api.Controllers;

public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("list")]
    public async Task<ActionResult<List<UserDto>>> ListAsync()
    {
        try
        {
            return await _mediator.Send(new GetUsersQuery());

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<UserDto>();
        }
    }

    [HttpGet("{id:required}")]
    public async Task<ActionResult<UserDto>> GetUserAsync(int id)
    {
        try
        {
            return await _mediator.Send(new GetUserQuery(id));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> CreateUserAsync(CreateUserCommand command) =>
        await _mediator.Send(command);
    
    [HttpPut("{id:required}")]
    public async Task<ActionResult<UserDto>> UpdateUserAsync(int id, UpdateUserCommand command)
    {
        try
        {
            return await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new UserDto();
        }
    }
    [HttpDelete("{id:required}")]
    public async Task<ActionResult<bool>> Delete(int id) =>
        await _mediator.Send(new DeleteUserCommand() { Id = id });

}