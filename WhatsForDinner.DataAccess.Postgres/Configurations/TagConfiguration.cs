using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatsForDinner.DataAccess.Postgres.Models;

namespace WhatsForDinner.DataAccess.Postgres.Configurations;

public class TagConfiguration: IEntityTypeConfiguration<TagEntity>
{
    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.HasKey(t => t.TagId);

        builder.Property(t => t.Type).HasConversion<string>().HasMaxLength(20);

        builder.HasOne(t => t.Dish)
            .WithMany(d => d.Tags)
            .HasForeignKey(t => t.DishId);

    }
}