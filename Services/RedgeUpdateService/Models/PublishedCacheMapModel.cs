using Umbraco.Cms.Core.Models.PublishedContent;

namespace KanUpdater.Services.RedgeUpdateService.Models
{
    public class PublishedCacheMapModel
    {
        public required IPublishedContent AssignedContent { get; set; }
        public IPublishedContent AssignedSubclass { get; set; }
    }
}
