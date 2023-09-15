using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AirQualityControlAPI.Infrastructure.Persistence
{
    public abstract class BaseDbContext : DbContext, IBaseDbContext
    {
        public BaseDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            BaseDbContext baseDbContext = this;
            bool hasChanges = baseDbContext.ChangeTracker.HasChanges();
            
            if (!hasChanges)
                return 0;
            
            foreach (EntityEntry<IAuditableEntity> entry in baseDbContext.ChangeTracker.Entries<IAuditableEntity>())
            {

                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastUpdated = DateTime.UtcNow;
                        continue;
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        continue;
                    default:
                        continue;
                }
            }

            return await baseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
