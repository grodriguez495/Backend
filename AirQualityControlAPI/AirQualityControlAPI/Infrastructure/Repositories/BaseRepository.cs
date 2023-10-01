using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain;
using AirQualityControlAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace AirQualityControlAPI.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TKey, TDBContext> :
        ICreateCommandRepository<TEntity, TKey>, IExtendedQueryRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TDBContext : IBaseDbContext
    {
        protected TDBContext _dbContext;
        public BaseRepository(TDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        protected abstract DbSet<TEntity> Entities { get; }
        protected abstract Expression<Func<TEntity, bool>> FindExpression(TKey key);
        protected abstract IQueryable<TEntity> DefaultIQueryable();

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool isTrackingEntities = false, CancellationToken cancellationToken = default)
        {
            return await FindAsync(DefaultIQueryable(), predicate, isTrackingEntities, cancellationToken);
        }

        public async Task<TEntity> FindAsync(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, bool isTrackingEntities = false, CancellationToken cancellationToken = default)
        {
            query = query.Where(predicate);

            if (!isTrackingEntities)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool isTrackingEntities = false,
        CancellationToken cancellationToken = default)
        {
            var result = await ExecuteListAsync(DefaultIQueryable(), predicate, isTrackingEntities, cancellationToken);
            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> ListAsync(
            IQueryable<TEntity> query, Expression<Func<TEntity, bool>>? predicate = null, bool isTrackingEntities = false, CancellationToken cancellationToken = default)
        {
           var result = await ExecuteListAsync(query,predicate,isTrackingEntities,cancellationToken);
            return result;  
        }

        public async Task<TEntity> FindAsync(TKey key, bool isTrackingEntity = false, CancellationToken cancellationToken = default)
        {
            var query = DefaultIQueryable().Where(FindExpression(key));

            if (!isTrackingEntity)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> RegisterAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            //return await this.TrySaveEntitiesAsync(cancellationToken);
            return await this.ExecuteWithTransactionAsync(async () => await this.ExecuteAddAsync(entity),
                cancellationToken);
        }

        protected virtual async Task<bool> ExecuteAddAsync(TEntity entity)
        {
            var trackedEntity = await this.Entities.AddAsync(entity);
            return trackedEntity.State == EntityState.Added;
        }

        protected async Task<bool> ExecuteWithTransactionAsync(Func<Task<bool>> action, CancellationToken cancellationToken)
        {
            if (!await action())
                return false;

            return await this.TrySaveEntitiesAsync(cancellationToken);
        }
        protected async Task<bool> ExecuteWithTransactionAsync(Func<bool> action, CancellationToken cancellationToken)
        {
            if (!action())
                return false;

            return await this.TrySaveEntitiesAsync(cancellationToken);
        }

        protected async Task<bool> TrySaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var changes = await this._dbContext.SaveEntitiesAsync(cancellationToken);
            return changes > 0;
        }

        public async Task<bool> EditAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await this.ExecuteWithTransactionAsync(() => this.ExecuteUpdate(entity),cancellationToken);
        }

        protected virtual bool ExecuteUpdate(TEntity entity)
        {
            var trackedEntity = this.Entities.Update(entity);

            return trackedEntity.State == EntityState.Modified;
        }

        public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await this.ExecuteWithTransactionAsync(() => this.ExecuteRemove(entity),cancellationToken);
        }

        protected bool ExecuteRemove(TEntity entity)
        {
          var trackedEntity = this.Entities.Remove(entity);
            return trackedEntity.State == EntityState.Deleted;
        }

        public async Task<List<TEntity>> ExecuteListAsync(IQueryable<TEntity> query,
          Expression<Func<TEntity, bool>>? predicate, bool isTrackingEntities, CancellationToken cancellationToken)
        {
            if (predicate != null)
                query = query.Where(predicate);

            if (!isTrackingEntities)
                query = query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }
    }
}
