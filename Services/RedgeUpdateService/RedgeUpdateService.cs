using KanUpdater.Services.RedgeUpdateService.Models;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;

namespace KanUpdater.Services.RedgeUpdateService
{
    public class RedgeUpdateService : IRedgeUpdateService
    {
        private readonly IContentService _contentService;
        private readonly IUmbracoMapper _mapper;
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        public RedgeUpdateService(IContentService contentService,
                                  IUmbracoMapper mapper,
                                  IUmbracoContextFactory umbracoContextFactory)
        {
            _contentService = contentService;
            _mapper = mapper;
            _umbracoContextFactory = umbracoContextFactory;
        }
        public RedgeUpdateRequestModel? GetContentBasedRedgeUpdateModel(int contentId)
        {
            var content = _contentService.GetById(contentId);

            if (content == null)
            {
                return null;
            }

            var contentAncestors = _contentService.GetAncestors(content);
            var res = _mapper.Map<RedgeUpdateRequestModel>(new ContentMapModel()
            {
                AssignedContent = content,
                AssignedSubclass = contentAncestors.FirstOrDefault(x => x.ContentType.Alias == "subClass")
            });

            return res;
        }

        public RedgeUpdateRequestModel? GetCachetBasedRedgeUpdateModel(int contentId)
        {
            using var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext();
            var content = umbracoContextReference.UmbracoContext.Content.GetById(contentId);

            if (content == null)
            {
                return null;
            }

            var contentAncestors = content.AncestorsOrSelf();
            var res = _mapper.Map<RedgeUpdateRequestModel>(new PublishedCacheMapModel()
            {
                AssignedContent = content,
                AssignedSubclass = contentAncestors.FirstOrDefault(x => x.ContentType.Alias == "subClass")
            });

            return res;
        }

        public IEnumerable<RedgeUpdateRequestModel?> GetContentBasedRedgeUpdateModels(IEnumerable<int> ids)
        {
            var contentItems = _contentService.GetByIds(ids.Distinct());

            if (contentItems == null || !contentItems.Any())
            {
                return null;
            }

            var itemsToMap = contentItems.Select(x =>
            {
                var contentAncestors = _contentService.GetAncestors(x);

                return new ContentMapModel()
                {
                    AssignedContent = x,
                    AssignedSubclass = x.ContentType.Alias == "subClass" ? x : contentAncestors.FirstOrDefault(x => x.ContentType.Alias == "subClass")
                };
            });

            var res = _mapper.MapEnumerable<ContentMapModel, RedgeUpdateRequestModel>(itemsToMap);

            return res;
        }

        public IEnumerable<RedgeUpdateRequestModel?> GetCacheBasedRedgeUpdateModels(IEnumerable<int> ids)
        {
            using var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext();
            var publishedItems = ids.Distinct().Select(x=> umbracoContextReference.UmbracoContext.Content.GetById(x));

            if (publishedItems == null || !publishedItems.Any())
            {
                return null;
            }

            var itemsToMap = publishedItems.Select(x =>
            {
                var contentAncestors = x.AncestorsOrSelf();

                return new PublishedCacheMapModel()
                {
                    AssignedContent = x,
                    AssignedSubclass = contentAncestors.FirstOrDefault(x => x.ContentType.Alias == "subClass")
                };
            });

            var res = _mapper.MapEnumerable<PublishedCacheMapModel, RedgeUpdateRequestModel>(itemsToMap);

            return res;
        }
    }
}
