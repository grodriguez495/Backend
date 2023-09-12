using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AirQualityControlAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public IMediator _mediator;
        public BaseController(IMediator sender) 
        {
            _mediator = sender;
        }
    }
}
