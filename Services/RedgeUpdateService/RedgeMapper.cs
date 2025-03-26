using KanUpdater.Services.RedgeUpdateService.Models;
using KanUpdater.Services.RedgeUpdateService.NodeTypeFactory;
using System.Globalization;
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
            mapper.Define<PublishedCacheMapModel, RedgeUpdateRequestModel>((source, context) => new RedgeUpdateRequestModel(), Map);
        }

        private void Map(PublishedCacheMapModel source, RedgeUpdateRequestModel target, MapperContext context)
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
                    Title = source.AssignedContent.Value<string>(childNodeType.TranslationTitle)
                }
            };

            target.Category = source.AssignedSubclass.Value<string>(childNodeType.Category);
            target.KlhCode = source.AssignedContent.Value<string>(childNodeType.KlhCode);
            target.ExternalCreated = source.AssignedContent.CreateDate.ToString("o", CultureInfo.InvariantCulture);
            target.ExternalModified = source.AssignedContent.UpdateDate.ToString("o", CultureInfo.InvariantCulture);
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

            target.Category = source.AssignedSubclass.GetValue<string>(childNodeType.Category);
            target.KlhCode = source.AssignedContent.GetValue<string>(childNodeType.KlhCode);
            target.ExternalCreated = source.AssignedContent.CreateDate.ToString("o", CultureInfo.InvariantCulture);
            target.ExternalModified = source.AssignedContent.UpdateDate.ToString("o", CultureInfo.InvariantCulture);
        }
    }
}
