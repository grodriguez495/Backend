using System.Linq.Expressions;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Alerts.Commands;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using AirQualityControlAPI.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Repositories;

public class AlertRepository:
    BaseRepository<AlertNotification,int,IAirQualityControlDbContext>,
    IAlertsQueryRepository,
    IAlertsCommandRepository
{
    public AlertRepository(IAirQualityControlDbContext dBContext) : base(dBContext)
    {
    }

    protected override DbSet<AlertNotification> Entities => _dbContext.AlertNotifications;
    protected override Expression<Func<AlertNotification, bool>> FindExpression(int key)
    {
        return alert => alert.AlertId == key;
    }

    protected override IQueryable<AlertNotification> DefaultIQueryable() => Entities.AsQueryable();
}