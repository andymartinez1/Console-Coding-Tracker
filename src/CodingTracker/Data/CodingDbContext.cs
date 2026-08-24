using CodingTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CodingTracker.Data;

public class CodingDbContext : DbContext
{
    public DbSet<CodingSession> CodingSessions { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlite(configuration.GetConnectionString("SQLiteConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Project>()
            .HasMany(p => p.CodingSessions)
            .WithOne(cs => cs.Project)
            .HasForeignKey(cs => cs.ProjectId)
            .IsRequired();
    }
}