using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Domain.Models;

public partial class AirQualityControlContext : DbContext
{
    public AirQualityControlContext()
    {
    }

    public AirQualityControlContext(DbContextOptions<AirQualityControlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<refreshtoken> refreshtokens { get; set; }

    public virtual DbSet<role> roles { get; set; }

    public virtual DbSet<user> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<refreshtoken>(entity =>
        {
            entity.HasKey(e => e.user_id);

            entity.ToTable("refreshtoken");

            entity.Property(e => e.user_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.ToTable("role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.user_id);

            entity.ToTable("user");

            entity.Property(e => e.email).HasMaxLength(50);
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.password).HasMaxLength(50);
            entity.Property(e => e.phone).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
