﻿using KanUpdater.Services.RedgeUpdateService.Models;
using KanUpdater.Services.RedgeUpdateService.NodeTypeFactory;
using Umbraco.Cms.Core.Mapping;

namespace KanUpdater.Services.RedgeUpdateService
{
    public class RedgeMapper : IMapDefinition
    {
        private readonly IChildNodeTypeFactoryRedge _childNodeTypeFactoryRedge;
        public RedgeMapper(IChildNodeTypeFactoryRedge childNodeTypeFactoryRedge)
        {
            _childNodeTypeFactoryRedge = childNodeTypeFactoryRedge;
        }
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<ContentMapModel, RedgeUpdateRequestModel>((source, context) => new RedgeUpdateRequestModel(), Map);
        }

        private void Map(ContentMapModel source, RedgeUpdateRequestModel target, MapperContext context)
        {
            target.ExternalId = source.AssignedContent.Id;
            var childNodeType = _childNodeTypeFactoryRedge.GetInstance(source.AssignedContent);

            if (childNodeType == null)
            {
                return;
            }

            target.Type = childNodeType.Type;
            target.Translations = new Translations
            {
                HEB = new Translation()
                {
                    Title = source.AssignedContent.GetValue<string>(childNodeType.TranslationTitle)
                }
            };
        }
    }
}
