using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Persistence
{
    public interface IAirQualityControlSchemaEntityTypeConfiguration<TBase>: IEntityTypeConfiguration<TBase> where TBase : class
    {
    }
}
