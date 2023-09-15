namespace AirQualityControlAPI.Infrastructure.Contexts;

public class DbContextMediatorFactory: IDbContextMediatorFactory
{
    public IDbContextMediator NewContextMediator() => new DbContextMediator();
}