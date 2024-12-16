using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WhatsForDinner.DataAccess.Postgres.Models;

public class ImageEntity
{
    [Column("image_id")]
    public Guid ImageId { get; init; } = Guid.CreateVersion7(TimeProvider.System.GetUtcNow());
    
    [Column("title")]
    [MaxLength(50)]
    public string Title { get; set; } = Path.GetRandomFileName();
    
    [Column("content")]
    public required byte[] Content { get; set; }
    
    [JsonIgnore]
    [Column("dish_id")]
    public Guid DishId { get; set; }
    
    [JsonIgnore]
    public DishEntity Dish { get; set; }
}