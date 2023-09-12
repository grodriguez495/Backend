using System.Linq.Expressions;

namespace AirQualityControlAPI.Domain.Repositories
{
    public interface IExtendedQueryRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey>
       where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool isTrackingEntities = false, CancellationToken cancellationToken = default);

        Task<TEntity> FindAsync(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, bool isTrackingEntities = false, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> ListAsync(IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> predicate = null, bool isTrackingEntities = false, CancellationToken cancellationToken = default);
    }
}
