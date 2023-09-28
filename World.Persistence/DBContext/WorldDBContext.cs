using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using World.Domain.DomainEntity.World;

namespace World.Persistence.DBContext;

public partial class WorldDBContext : DbContext
{
    public WorldDBContext(DbContextOptions<WorldDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryLanguage> CountryLanguages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_City_ID");

            entity.Property(e => e.CountryCode)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.District)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("City$city_ibfk_1");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_Country_Code");

            entity.Property(e => e.Code)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.Code2)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.Continent).HasDefaultValueSql("(N'Asia')");
            entity.Property(e => e.GovernmentForm)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.HeadOfState).IsFixedLength();
            entity.Property(e => e.LocalName)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.Region)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.SurfaceArea).HasDefaultValueSql("((0.00))");
        });

        modelBuilder.Entity<CountryLanguage>(entity =>
        {
            entity.HasKey(e => new { e.CountryCode, e.Language }).HasName("PK_CountryLanguage_CountryCode");

            entity.Property(e => e.CountryCode)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.Language)
                .HasDefaultValueSql("(N'')")
                .IsFixedLength();
            entity.Property(e => e.IsOfficial).HasDefaultValueSql("(N'F')");

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.CountryLanguages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CountryLanguage$countryLanguage_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
