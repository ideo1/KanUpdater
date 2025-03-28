using KanUpdater.Models;
using KanUpdater.Services.RedgeUpdateService.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public class ProgramNodeTypeRedge : ChildNodeTypeRedge
    {
        public ProgramNodeTypeRedge()
        {
            TranslationTitle = "title";
            Type = Enum.RedgeContentType.SERIES;
            TranslationDescription = "programContent";
            Platform = "platform";
            AgeRestriction = "programAgeRestrictions";
            CustomLabel = "roofTitle";
            MediaItemsMap = new List<MediaItemMap>()
            {
                new MediaItemMap()
                {
                    Aspect = Enum.RedgeImageAspect._16x9,
                    Type = Enum.RedgeImageType.COVER,
                    Crop = ImageCrops.FullHDBackground,
                    Alias = "programCoverImage"
                }, 
                new MediaItemMap() 
                {
                    Aspect = Enum.RedgeImageAspect._3x4,
                    Type = Enum.RedgeImageType.POSTER,
                    Crop = ImageCrops.AspectRatio13x20,
                    Alias ="heroMobile"
                }
            };
        }

        public override Dictionary<string, IContent> GetGenreTagsConetntMap(ContentMapModel mapModel)
        {
            return new Dictionary<string, IContent>() { { "programGenre", mapModel.AssignedContent } };
        }

        public override Dictionary<string, IPublishedContent> GetGenreTagsCacheMap(PublishedCacheMapModel mapModel)
        {
            return new Dictionary<string, IPublishedContent>() { { "programGenre", mapModel.AssignedContent } };
        }

        public override Dictionary<string, IContent> GetGeneralTagsContentMap(ContentMapModel mapModel)
        {
            return new Dictionary<string, IContent>() { { "generalTags", mapModel.AssignedContent } };
        }

        public override Dictionary<string, IPublishedContent> GetGeneralTagsCacheMap(PublishedCacheMapModel mapModel)
        {
            return new Dictionary<string, IPublishedContent>() { { "generalTags", mapModel.AssignedContent } };
        }
    }
}