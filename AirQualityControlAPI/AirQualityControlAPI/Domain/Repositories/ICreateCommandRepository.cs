namespace AirQualityControlAPI.Domain.Repositories
{
    public interface ICreateCommandRepository<T, TKey> where T : BaseEntity<TKey>
    {
        Task<bool> RegisterAsync(T entity, CancellationToken cancellationToken = default(CancellationToken)); 
    }
}
