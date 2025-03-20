using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KanUpdater.Services.RedgeUpdateService.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedgeImageType
    {
        COVER,
        LOGO,
        POSTER,
        GALLERY,
        TITLE_TREATMENT
    }
}
