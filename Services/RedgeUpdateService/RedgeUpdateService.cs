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

            var res  = _mapper.Map<RedgeUpdateRequestModel>(new ContentMapModel() { AssignedContent = content});

            return res;
        }
    }
}
