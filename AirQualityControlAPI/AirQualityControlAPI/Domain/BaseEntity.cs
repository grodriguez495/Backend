using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AirQualityControlAPI.Domain
{
    public abstract class BaseEntity<TIdentity>: IEntity
    {
    public abstract TIdentity GetIdentity();
    }
}
