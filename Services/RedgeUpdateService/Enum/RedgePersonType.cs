using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KanUpdater.Services.RedgeUpdateService.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedgePersonType
    {
        ACTOR,
        AUTHOR,
        COMPOSER,
        DIRECTOR,
        PRESENTER,
        PRODUCER,
        SCRIPTWRITER
    }
}