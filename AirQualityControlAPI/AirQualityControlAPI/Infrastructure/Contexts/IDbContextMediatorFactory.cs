namespace AirQualityControlAPI.Infrastructure.Contexts;

public interface IDbContextMediatorFactory
{
    IDbContextMediator NewContextMediator();
}