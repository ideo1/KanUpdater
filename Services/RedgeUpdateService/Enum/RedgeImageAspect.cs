using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace KanUpdater.Services.RedgeUpdateService.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedgeImageAspect
    {
        [Display(Name = "16x9")]
        _16x9,
        [Display(Name = "3x4")]
        _3x4,
        [Display(Name = "1x1")]
        _1x1
    }
}