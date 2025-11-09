using CodingTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CodingTracker.Data;

public class CodingDbContext : DbContext
{
    public DbSet<CodingSession> CodingSessions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Project>()
            .HasMany(p => p.CodingSessions)
            .WithOne(cs => cs.Project)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder
            .Entity<ProgrammingLanguage>()
            .HasMany(pl => pl.CodingSessions)
            .WithMany(cs => cs.ProgrammingLanguages);

        modelBuilder
            .Entity<Project>()
            .HasMany(p => p.ProgrammingLanguages)
            .WithMany(pl => pl.Projects);
    }
}
