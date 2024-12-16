using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WhatsForDinner.DataAccess.Postgres.Enums;

namespace WhatsForDinner.DataAccess.Postgres.Models;

public class TagEntity
{
    [Column("tag_id")]
    public Guid TagId { get; set; } = Guid.CreateVersion7(TimeProvider.System.GetUtcNow());
    
    [Column("title")]
    [MaxLength(50)]
    public required string Title { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Column("type")]
    
    public TagType Type { get; set; }
    
    [JsonIgnore]
    public DishEntity Dish { get; set; }
    
    [JsonIgnore]
    [Column("dish_id")]
    public Guid DishId { get; set; }
}