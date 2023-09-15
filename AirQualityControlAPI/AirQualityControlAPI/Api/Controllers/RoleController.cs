using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Application.Features.Roles.Commands.CreateRoles;
using AirQualityControlAPI.Application.Features.Roles.Commands.DeleteRoles;
using AirQualityControlAPI.Application.Features.Roles.Commands.UpdateRole;
using AirQualityControlAPI.Application.Features.Roles.Queries.GetRole;
using AirQualityControlAPI.Application.Features.Roles.Queries.GetRoles;
using AirQualityControlAPI.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace AirQualityControlAPI.Api.Controllers
{
    public class RoleController : BaseController
    {

        public RoleController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<RoleDto>>> ListAsync()
        {
            try
            {
                return await _mediator.Send(new GetRolesQuery());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<RoleDto>();
            }
        }


        [HttpGet("{id:required}")]
        public async Task<ActionResult<RoleDto>> GetRoleAsync(int id)
        {
            try
            {
                return await _mediator.Send(new GetRoleQuery(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new RoleDto();
            }
        }


        [HttpPost]
        public async Task<ActionResult<bool>> CreateRoleAsync(CreateRoleCommand command) =>
            await _mediator.Send(command);


        [HttpPut("{id:required}")]
        public async Task<ActionResult<RoleDto>> UpdateRoleAsync(int id, UpdateRoleCommand command)
        {
            try
            {
                command.RoleId = id;
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new RoleDto();
            }
        }


        [HttpDelete("{id:required}")]
        public async Task<ActionResult<bool>> Delete(int id) =>
            await _mediator.Send(new DeleteRoleCommand { Id = id });
    }
}
