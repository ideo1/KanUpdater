using KanUpdater.Services.RedgeUpdateService.Models;
using Umbraco.Cms.Core.Services;

namespace KanUpdater.Services.RedgeUpdateService
{
    public class RedgeUpdateService : IRedgeUpdateService
    {
        private readonly IContentService _contentService;
        public RedgeUpdateService(IContentService contentService)
        {
            _contentService = contentService;
        }
        public RedgeUpdateRequestModel GetRedgeUpdateModel(int contentId)
        {
            var content = _contentService.GetById(contentId);

            if (content == null)
            {
                return null;
            }

            return new RedgeUpdateRequestModel() 
            {
                ExternalId = content.Id,
                Type = Enum.RedgeContentType.ARTICLE
            };
        }
    }
}
