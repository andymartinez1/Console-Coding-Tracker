using System.Data;
using CodingTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTracker.Data;

public class CodingDbContext : DbContext
{
    internal readonly IDbConnection ConnectionString;

    public CodingDbContext(IDbConnection connectionString)
    {
        ConnectionString = connectionString;
    }

    public DbSet<CodingSession> CodingSessions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Project>()
            .HasMany(p => p.CodingSessions)
            .WithOne(cs => cs.Project)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder
            .Entity<Project>()
            .HasMany(p => p.ProgrammingLanguages)
            .WithMany(pl => pl.Projects);

        modelBuilder
            .Entity<ProgrammingLanguage>()
            .HasMany(pl => pl.CodingSessions)
            .WithMany(cs => cs.ProgrammingLanguages);
    }
}
