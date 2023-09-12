namespace AirQualityControlAPI.Application.Interfaces
{
    public interface IBaseDbContext
    {
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken);
    }
}
