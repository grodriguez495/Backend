using System.Linq.Expressions;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Sensors.Commands;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AirQualityControlAPI.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Repositories;

public class SensorRepository:
    BaseRepository<Sensor,string,IAirQualityControlDbContext>,
    ISensorQueryRepository,
    ISensorCommandRepository
{
    public SensorRepository(IAirQualityControlDbContext dBContext) : base(dBContext)
    {
    }

    protected override DbSet<Sensor> Entities => _dbContext.Sensor;
    protected override Expression<Func<Sensor, bool>> FindExpression(string key)
    {
        return sensor => sensor.SensorId == key;
    }

    protected override IQueryable<Sensor> DefaultIQueryable() => Entities.AsQueryable();
}