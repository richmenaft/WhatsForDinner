using System.Text.Json.Serialization;

namespace WhatsForDinner.Models;

public class Image
{
    public Guid ImageId { get; init; }
    
    public required string Title { get; set; }
    
    public required string Content { get; set; }
    
    [JsonIgnore]
    public Guid DishId { get; set; }
}