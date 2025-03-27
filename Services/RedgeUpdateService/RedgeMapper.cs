using KanUpdater.Services.RedgeUpdateService.Models;
using KanUpdater.Services.RedgeUpdateService.NodeTypeFactory;
using System.Globalization;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace KanUpdater.Services.RedgeUpdateService
{
    public class RedgeMapper : IMapDefinition
    {
        private readonly IChildNodeTypeFactoryRedge _childNodeTypeFactoryRedge;
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        public RedgeMapper(IChildNodeTypeFactoryRedge childNodeTypeFactoryRedge, IUmbracoContextFactory umbracoContextFactory)
        {
            _childNodeTypeFactoryRedge = childNodeTypeFactoryRedge;
            _umbracoContextFactory = umbracoContextFactory;
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

            var genres = source.AssignedContent.Value<IEnumerable<IPublishedContent>>(childNodeType.Genres);

            if (genres != null)
            {
                target.Genres = genres.Select(x => x.Name);
            }

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

            var genresContent = GetContentPickerData(source.AssignedContent, childNodeType.Genres);

            if (genresContent != null && genresContent.Any())
            {
                target.Genres = genresContent.Select(x => x.Name);
            }

        }

        private IEnumerable<IPublishedContent> GetContentPickerData(IContent content, string pickerField)
        {
            var pickerData = content.GetValue<string>(pickerField);

            if (string.IsNullOrEmpty(pickerData))
            {
                return null;
            }

            using var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext();
            var res = pickerData.Split(',')
                .Select(x =>
                    {
                        var guidString = x.Replace("umb://document/", "");

                        if (!Guid.TryParse(guidString, out var guid))
                        {
                            return null;
                        }

                        return umbracoContextReference.UmbracoContext.Content.GetById(guid);
                    })
                .WhereNotNull();

            return res;
        }
    }
}
