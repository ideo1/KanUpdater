using KanUpdater.Services.RedgeUpdateService.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public class VideoNodeTypeRedge : ChildNodeTypeRedge
    {
        public override Dictionary<string, IContent> GetGenreTagsConetntMap(ContentMapModel mapModel)
        {
            return new Dictionary<string, IContent>() { 
                { "programGenre", mapModel.AssignedProgram }, 
                { "genreTags", mapModel.AssignedContent } 
            };
        }

        public override Dictionary<string, IPublishedContent> GetGenreTagsCacheMap(PublishedCacheMapModel mapModel)
        {
            return new Dictionary<string, IPublishedContent>() 
            { 
                { "programGenre", mapModel.AssignedProgram },
                { "genreTags", mapModel.AssignedContent }
            };
        }

        public override Dictionary<string, IContent> GetGeneralTagsContentMap(ContentMapModel mapModel)
        {
            return new Dictionary<string, IContent>() { 
                { "generalTags", mapModel.AssignedContent },
                { "generalTags", mapModel.AssignedProgram }
            };
        }

        public override Dictionary<string, IPublishedContent> GetGeneralTagsCacheMap(PublishedCacheMapModel mapModel)
        {
            return new Dictionary<string, IPublishedContent>() 
            { 
                { "generalTags", mapModel.AssignedContent },
                { "generalTags", mapModel.AssignedProgram }
            };
        }
    }
}
