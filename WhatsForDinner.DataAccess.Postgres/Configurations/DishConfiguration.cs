using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatsForDinner.DataAccess.Postgres.Models;

namespace WhatsForDinner.DataAccess.Postgres.Configurations;

public class DishConfiguration : IEntityTypeConfiguration<DishEntity>
{
    public void Configure(EntityTypeBuilder<DishEntity> builder)
    {
        builder.HasKey(d => d.DishId);
        
        builder.HasOne(d => d.Img)
            .WithOne(i => i.Dish)
            .HasForeignKey<DishEntity>(d => d.ImageId);

        builder.HasMany(d => d.Tags)
            .WithOne(t => t.Dish)
            .HasForeignKey(t => t.DishId);

    }
}