using System.Text.Json.Serialization;

namespace exam_webapi.Models
{
    /// <summary>
    /// Represents the recognized types of User
    /// </summary>
    [JsonConverter (typeof(JsonStringEnumConverter))]
    public enum UserType_Enum
    {
        Soldier = 0,
        Thief,
        Mage,
    }
}