using Cine.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cine.Repository;

public class CineDbContext(DbContextOptions<CineDbContext> options) : DbContext(options)
{
    public virtual DbSet<Movie> Movies => Set<Movie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();
            entity.Property(m => m.Title).IsRequired();
        });
    }
}
