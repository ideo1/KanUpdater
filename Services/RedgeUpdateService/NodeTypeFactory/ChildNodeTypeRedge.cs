using KanUpdater.Services.RedgeUpdateService.Enum;
using KanUpdater.Services.RedgeUpdateService.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public class ChildNodeTypeRedge
    {
        public string TranslationTitle { get; set; } = "";
        public string TranslationDescription { get; set; } = "";
        public string KlhCode { get; set; } = "kLHCode";
        public string Category { get; set; } = "title";
        public string AgeRestriction { get; set; } = "";
        public string Platform { get; set; } = "";
        public string CustomLabel { get; set; } = "";
        public RedgeContentType Type { get; set; }
        public virtual Dictionary<string, IContent> GetGenreTagsConetntMap(ContentMapModel mapModel) => new Dictionary<string, IContent>();
        public virtual Dictionary<string, IPublishedContent> GetGenreTagsCacheMap(PublishedCacheMapModel mapModel) => new Dictionary<string, IPublishedContent>();
        public virtual Dictionary<string, IContent> GetGeneralTagsContentMap(ContentMapModel mapModel) => new Dictionary<string, IContent>();
        public virtual Dictionary<string, IPublishedContent> GetGeneralTagsCacheMap(PublishedCacheMapModel mapModel) => new Dictionary<string, IPublishedContent>();
    }
}
