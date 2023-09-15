namespace AirQualityControlAPI.Domain.Repositories;

public interface IUpdateCommandRepository<TEntity,TIdentity> where TEntity: BaseEntity<TIdentity>
{
    Task<bool> EditAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

}