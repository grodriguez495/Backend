using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace AirQualityControlAPI.Api.Controllers
{
    public class RoleController : BaseController
    {
      
        public RoleController(IMediator mediator) :base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<RoleDto>>> ListAsync()
        {
            try
            {
               return await _mediator.Send(new GetRoleQuery());

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        } 

        /*
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

       
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

       
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
