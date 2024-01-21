using System.Linq.Expressions;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Alerts.Commands;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using AirQualityControlAPI.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Repositories;

public class AlertsRepository: BaseRepository<AlertNotification,int,IAirQualityControlDbContext>,
    IAlertsQueryRepository,
    IAlertsCommandRepository
{
    public AlertsRepository(IAirQualityControlDbContext dBContext) : base(dBContext)
    {
    }

    protected override DbSet<AlertNotification> Entities => _dbContext.Alerts;
    protected override Expression<Func<AlertNotification, bool>> FindExpression(int key)
    {
        return alert => alert.AlertId == key;
    }

    protected override IQueryable<AlertNotification> DefaultIQueryable() => Entities.AsQueryable();
}