using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WhatsForDinner.DataAccess.Postgres.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TagType
{
    [Description("Ингредиент")]
    Ingredient,
    [Description("Тип")]
    Type,
    [Description("Повод")]
    Reason
}