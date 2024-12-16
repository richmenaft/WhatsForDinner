using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatsForDinner.DataAccess.Postgres.Models;

namespace WhatsForDinner.DataAccess.Postgres.Configurations;

public class ImageConfiguration: IEntityTypeConfiguration<ImageEntity>
{
    public void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        builder.HasKey(i => i.ImageId);
        
        builder.HasOne(i => i.Dish)
            .WithOne(d => d.Img)
            .HasForeignKey<ImageEntity>(i => i.DishId);
    }
}