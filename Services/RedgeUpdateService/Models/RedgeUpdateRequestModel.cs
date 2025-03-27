using KanUpdater.Services.RedgeUpdateService.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KanUpdater.Services.RedgeUpdateService.Models
{
    public class RedgeUpdateRequestModel : RedgeUpdateRequestModelBase
    {
        [JsonProperty("parent_uid")]
        public string ParentId { get; set; }
        [JsonProperty("translations")]
        public Translations Translations { get; set; }
        [JsonProperty("originalTitle")]
        public string OriginalTitle { get; set; }
        [JsonProperty("epgUuid")]
        public string KlhCode { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("rating")]
        public int AgeRestriction { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("introStart")]
        public int IntroStart { get; set; }

        [JsonProperty("introEnd")]
        public int IntroEnd { get; set; }

        [JsonProperty("creditsStart")]
        public int CreditsStart { get; set; }

        [JsonProperty("recapStart")]
        public int RecapStart { get; set; }

        [JsonProperty("recapEnd")]
        public int RecapEnd { get; set; }
        [JsonProperty("since")]
        public DateTime Since { get; set; }

        [JsonProperty("till")]
        public DateTime? Till { get; set; }
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("hd")]
        public bool Hd { get; set; }

        [JsonProperty("uhd")]
        public bool Uhd { get; set; }

        [JsonProperty("restrictToLoggedIn")]
        public bool RestrictToLoggedIn { get; set; }
        [JsonProperty("advisors")]
        public IEnumerable<string> Advisors { get; set; }
        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("production")]
        public string Production { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("additionalCategories")]
        public IEnumerable<string> AdditionalCategories { get; set; }

        [JsonProperty("genres")]
        public IEnumerable<string> Genres { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }

        [JsonProperty("productionCountries")]
        public List<string> ProductionCountries { get; set; }
        [JsonProperty("customLabel")]
        public string CustomLabel { get; set; }

        [JsonProperty("platforms")]
        [JsonConverter(typeof(StringEnumConverter))]
        public IEnumerable<RedgePlatform> Platforms { get; set; }
        [JsonProperty("chromecastPlatforms")]
        [JsonConverter(typeof(StringEnumConverter))]
        public IEnumerable<RedgeChromecastPlatform> ChromecastPlatforms { get; set; }
        [JsonProperty("mediaFiles")]
        public IEnumerable<MediaFile> MediaFiles { get; set; }
        [JsonProperty("imageFiles")]
        public IEnumerable<ImageFile> ImageFiles { get; set; }
        [JsonProperty("persons")]
        public IEnumerable<Person> Persons { get; set; }

    }

    public class Translations
    {
        [JsonProperty("HEB")]
        public Translation HEB { get; set; }

    }
    public class Translation
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("lead")]
        public string? Lead { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
    }

    public class MediaFile
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("format")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RedgeMediaFormat Format { get; set; }

        [JsonProperty("group")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RedgeMediaGroup Group { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RedgeMediaType Type { get; set; }
    }

    public  class Person
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("image")]
        public string ImageUrl { get; set; }
    }

    public class ImageFile
    {
        [JsonProperty("aspect")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RedgeImageAspect Aspect { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RedgeImageType Type { get; set; }

        [JsonProperty("url")]
        public string ImageUrl { get; set; }
    }
}
