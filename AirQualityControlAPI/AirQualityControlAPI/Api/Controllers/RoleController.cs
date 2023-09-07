using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirQualityControlAPI.Api.Controllers
{
    public class RoleController : BaseController
    {

        public RoleController() 
        {
        }

        
        [HttpGet("list")]
        public async Task<ActionResult<List<RoleDto>>> ListAsync() => await Mediator.Send(new GetRoleQuery());
            

        
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
        }
    }
}
