namespace AirQualityControlAPI.Domain.Repositories
{
    public interface ICommandRepository<T, TKey> where T : BaseEntity<TKey>
    {
        Task<bool> RegisterAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> EditAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> UnregisterAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
