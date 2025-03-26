using KanUpdater.Services.RedgeUpdateService.Models;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Services;

namespace KanUpdater.Services.RedgeUpdateService
{
    public class RedgeUpdateService : IRedgeUpdateService
    {
        private readonly IContentService _contentService;
        private readonly IUmbracoMapper _mapper;
        public RedgeUpdateService(IContentService contentService,
                                  IUmbracoMapper mapper)
        {
            _contentService = contentService;
            _mapper = mapper;
        }
        public RedgeUpdateRequestModel? GetRedgeUpdateModel(int contentId)
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

        public IEnumerable<RedgeUpdateRequestModel?> GetRedgeUpdateModels(IEnumerable<int> ids)
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
    }
}
