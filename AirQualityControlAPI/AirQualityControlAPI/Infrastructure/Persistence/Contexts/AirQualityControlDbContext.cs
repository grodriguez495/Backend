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
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Sensor> Sensor { get; set; }

    public bool ModelCreated { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      _dbContextMediator.ApplyConfigurations(modelBuilder);
    }
}
