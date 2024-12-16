using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WhatsForDinner.DataAccess.Postgres.Models;

public class DishEntity
{
    [Column("dish_id")]
    public Guid DishId { get; init; } = Guid.CreateVersion7(TimeProvider.System.GetUtcNow());
    
    [MaxLength(40)]
    [Column("title")]
    public required string Title { get; set; }
    
    [Column("description")]
    [MaxLength(255)]
    public string? Description { get; set; }
    
    public List<TagEntity>? Tags { get; set; } = [];
    
    [Column("image")]
    public ImageEntity? Img { get; set; } 
    
    [JsonIgnore]
    [Column("image_id")]
    public Guid? ImageId { get; set; }
    
    [NotMapped] public string? StringBase64 { get; set; }
}