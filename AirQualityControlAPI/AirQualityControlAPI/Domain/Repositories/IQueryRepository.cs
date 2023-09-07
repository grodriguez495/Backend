namespace AirQualityControlAPI.Domain.Repositories
{
    public interface IQueryRepository<T,TKey> where T : BaseEntity<TKey>
    {
    }
}
