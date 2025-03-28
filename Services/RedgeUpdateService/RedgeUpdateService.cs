using KanUpdater.Services.RedgeUpdateService.Models;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models;
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

            var contentAncestors = _contentService.GetAncestors(content).OrderBy(x=>x.Level);
            var res = _mapper.Map<RedgeUpdateRequestModel>(new ContentMapModel()
            {
                AssignedContent = content,
                AssignedSubclass = GetAncestorOrSelf(content, contentAncestors, "subClass"),
                AssignedProgram = GetAncestorOrSelf(content, contentAncestors, "program"),
                Root = contentAncestors.FirstOrDefault()
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
                AssignedSubclass = contentAncestors.FirstOrDefault(content => content.ContentType.Alias == "subClass"),
                AssignedProgram = contentAncestors.FirstOrDefault(content => content.ContentType.Alias == "program"),
                Root = contentAncestors.FirstOrDefault()
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
                var contentAncestors = _contentService.GetAncestors(x).OrderBy(x=>x.Level);

                return new ContentMapModel()
                {
                    AssignedContent = x,
                    AssignedSubclass = GetAncestorOrSelf(x, contentAncestors, "subClass"),
                    AssignedProgram = GetAncestorOrSelf(x, contentAncestors, "program"),
                    Root = contentAncestors.FirstOrDefault()
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
                    AssignedSubclass = contentAncestors.FirstOrDefault(x => x.ContentType.Alias == "subClass"),
                    AssignedProgram = contentAncestors.FirstOrDefault(content => content.ContentType.Alias == "program"),
                    Root = x.Root()
                };
            });

            var res = _mapper.MapEnumerable<PublishedCacheMapModel, RedgeUpdateRequestModel>(itemsToMap);

            return res;
        }

        private IContent GetAncestorOrSelf(IContent content, IEnumerable<IContent> contentAncestors, string doctype) =>
            content.ContentType.Alias == doctype ? content : contentAncestors.FirstOrDefault(content => content.ContentType.Alias == doctype);

    }
}
