using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KanUpdater.Services.RedgeUpdateService.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedgePlatform
    {
        SMART_TV,
        APPLE_TV,
        ANDROID_TV,
        ANDROID,
        IOS,
        ANDROID_KIDS,
        IOS_KIDS
    }
}
