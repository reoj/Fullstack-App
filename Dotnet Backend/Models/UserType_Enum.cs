using System.Text.Json.Serialization;

namespace exam_webapi.Models
{
    [JsonConverter (typeof(JsonStringEnumConverter))]
    public enum UserType_Enum
    {
        Soldier = 0,
        Thief,
        Mage,
    }
}