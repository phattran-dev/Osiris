using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Constants
{
    public static class JsonSerializerOptionConstans
    {

        public static readonly JsonSerializerOptions Default = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

    }
}
