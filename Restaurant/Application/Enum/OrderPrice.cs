using System.Text.Json.Serialization;

namespace Application.Enum
{
    public enum OrderPrice
    {

        [JsonConverter(typeof(JsonStringEnumConverter))]
        asc = 0,
        desc = 1
    }
}
