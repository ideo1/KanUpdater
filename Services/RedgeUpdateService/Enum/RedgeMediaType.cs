using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KanUpdater.Services.RedgeUpdateService.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedgeMediaType
    {
        VIDEO,
        AUDIO
    }
}
