using Microsoft.EntityFrameworkCore;
using SearchSystemInfrastructure.FileRepositories.ServicesData.DTOs;

namespace SearchSystemInfrastructure;

public class SearchSystemDbContext : DbContext
{
    public DbSet<ServiceInfoDatabaseDto> Services { get; set; }

    public SearchSystemDbContext(DbContextOptions<SearchSystemDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ServiceInfoDatabaseDto>(entity =>
        {
            entity.OwnsOne(e => e.Position);
        });
    }
}
