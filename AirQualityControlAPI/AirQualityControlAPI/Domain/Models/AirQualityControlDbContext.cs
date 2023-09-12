using System;
using System.Collections.Generic;
using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Domain.Models;

public partial class AirQualityControlDbContext : BaseDbContext, IAirQualityControlDbContext
{
    public AirQualityControlDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public virtual DbSet<Role> Roles {get;set;}

    public bool ModelCreated { get; set;}

    public void OnModelCreatingFromContext(ModelBuilder modelBuilder)
    {
      OnModelCreating(modelBuilder);
    }
}
