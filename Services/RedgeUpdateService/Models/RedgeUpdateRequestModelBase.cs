using KanUpdater.Services.RedgeUpdateService.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KanUpdater.Services.RedgeUpdateService.Models
{
    public abstract class RedgeUpdateRequestModelBase
    {
        [JsonProperty("externalUuid")]
        public int ExternalId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public RedgeContentType Type { get; set; }
        [JsonProperty("externalCreated")]
        public DateTime ExternalCreated { get; set; }

        [JsonProperty("externalModified")]
        public DateTime ExternalModified { get; set; }
    }
}
