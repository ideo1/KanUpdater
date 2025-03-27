using KanUpdater.Services.RedgeUpdateService.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KanUpdater.Services.RedgeUpdateService.Models
{
    public abstract class RedgeUpdateRequestModelBase
    {
        [JsonProperty("externalUuid")]
        public string ExternalId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public RedgeContentType Type { get; set; }
        [JsonProperty("externalCreated")]
        public string ExternalCreated { get; set; }

        [JsonProperty("externalModified")]
        public string ExternalModified { get; set; }
    }
}
