using System.Linq.Expressions;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.AlertTypes.Commands;
using AirQualityControlAPI.Domain.Repositories.AlertTypes.Queries;
using AirQualityControlAPI.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Repositories;

public class AlertTypeRepository : BaseRepository<AlertType,int,IAirQualityControlDbContext>,
    IAlertTypesQueryRepository,
    IAlertTypesCommandRepository
{
    public AlertTypeRepository(IAirQualityControlDbContext dBContext) : base(dBContext)
    {
    }

    protected override DbSet<AlertType> Entities => _dbContext.AlertTypes;
    protected override Expression<Func<AlertType, bool>> FindExpression(int key)
    {
        return type => type.Id == key;
        
    }

    protected override IQueryable<AlertType> DefaultIQueryable() => Entities.AsQueryable();
}