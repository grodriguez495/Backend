using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Persistence.Contexts;

public class AirQualityControlDbContext : BaseDbContext, IAirQualityControlDbContext
{
    private readonly IDbContextMediator _dbContextMediator;
    public AirQualityControlDbContext(DbContextOptions<AirQualityControlDbContext> dbContextOptions,IDbContextMediator contextMediator)
        : base(dbContextOptions)
    {
        _dbContextMediator = contextMediator ?? throw new ArgumentNullException(nameof(contextMediator));
    }

    public virtual DbSet<Role> Role {get;set;}

    public bool ModelCreated { get; set;}

    public void OnModelCreatingFromContext(ModelBuilder modelBuilder)
    {
      OnModelCreating(modelBuilder);
      _dbContextMediator.ApplyConfigurations(modelBuilder);
    }
}
