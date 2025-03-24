﻿using KanUpdater.Services.RedgeUpdateService.Enum;
using KanUpdater.Services.RedgeUpdateService.Models;
using Umbraco.Cms.Core.Services;
using static System.Net.Mime.MediaTypeNames;

namespace KanUpdater.Services.RedgeUpdateService
{
    public class RedgeUpdateService : IRedgeUpdateService
    {
        private readonly IContentService _contentService;
        public RedgeUpdateService(IContentService contentService)
        {
            _contentService = contentService;
        }
        public RedgeUpdateRequestModel? GetRedgeUpdateModel(int contentId)
        {
            var content = _contentService.GetById(contentId);

            if (content == null)
            {
                return null;
            }

            return new RedgeUpdateRequestModel() 
            {
                ExternalId = content.Id,
                Type = Enum.RedgeContentType.ARTICLE,
                Platforms = new List<RedgePlatform>() { RedgePlatform.IOS_KIDS }
            };
        }
    }
}
