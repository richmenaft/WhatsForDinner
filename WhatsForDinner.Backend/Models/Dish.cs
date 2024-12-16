using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WhatsForDinner.DataAccess.Postgres.Models;

namespace WhatsForDinner.Models;

public class Dish
{
    public Guid DishId { get; init; }
    
    [MaxLength(40)]
    public required string Title { get; set; }
    
    [MaxLength(255)]
    public string? Description { get; set; }
    
    public List<TagEntity>? Tags { get; set; } = [];
    
    public Image? Img { get; set; } 
    
    [JsonIgnore]
    public Guid? ImageId { get; set; }
}