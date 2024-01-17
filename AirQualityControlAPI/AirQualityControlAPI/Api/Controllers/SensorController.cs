using AirQualityControlAPI.Application.Features.Sensors.Commands;
using AirQualityControlAPI.Application.Features.Sensors.Queries;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorByDateAndVariables;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensors;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorValuesBydatesAndVariables;
using AirQualityControlAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirQualityControlAPI.Api.Controllers;

public class SensorController : BaseController
{
    public SensorController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> UploadJsonAsync(IFormFile file)
    {
        try
        {
            UploadJsonSensorCommand jsonSensorCommand = new()
            {
                PostedFile = file.OpenReadStream()
            };
            return await _mediator.Send(jsonSensorCommand);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<SensorDto>>> GetValueSensorListAsync()
    {
        try
        {
            return await _mediator.Send(new GetSensorVariablesQuery());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<SensorDto>();
        }
    }

    [HttpGet("By-variable-and-date-option")]
    public async Task<ActionResult<List<SensorDto>>> GetValueSensorByDateAndVariable(int variableId,int dateId)
    {
        try
        {
            return await _mediator.Send(new GetSensorByDateAndVariableQuery(variableId,dateId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<SensorDto>();
        }
    }

    [HttpGet("By-dates-and-variable")]
    public async Task<ActionResult<List<SensorDto>>> GetValueSensorByDatesAndVariableId(int variableId, string dateFrom,
        string dateTo)
    {
        try
        {
            return await _mediator.Send(new GetSensorValuesByDatesAndVariablesQuery(dateFrom,dateTo,variableId));

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<SensorDto>();
        }
    }
}