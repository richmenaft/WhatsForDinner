using Microsoft.EntityFrameworkCore;
using WhatsForDinner.DataAccess.Postgres.Configurations;
using WhatsForDinner.DataAccess.Postgres.Models;

namespace WhatsForDinner.DataAccess.Postgres;

public class WfdDbContext(DbContextOptions<WfdDbContext> options): DbContext (options)
{
    public DbSet<DishEntity> Dishes { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<TagEntity> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DishConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new ImageConfiguration());

        foreach (var item in modelBuilder.Model.GetEntityTypes())
        {
            var p = item.FindPrimaryKey().Properties.FirstOrDefault(i=>i.ValueGenerated!=Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never);
            if (p!=null)
            {
                p.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never;
            }
        }
    }
}