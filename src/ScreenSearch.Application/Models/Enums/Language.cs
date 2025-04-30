using System.Text.Json.Serialization;

namespace ScreenSearch.Application.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter<Language>))]
    public enum Language
    {
        English,
        German
    }
}
