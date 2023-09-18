using System.Linq.Expressions;

namespace AirQualityControlAPI.Domain.Repositories
{
    public interface IQueryRepository<T,TKey> where T : BaseEntity<TKey>
    {
        Task<T> FindAsync(TKey key, bool isTrackingEntity = false, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> ListAsync(
            Expression<Func<T, bool>>? Predicate = null,
            bool isTrackingEntity= false,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
