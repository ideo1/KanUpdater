using KanUpdater.Services.Extensions;
using KanUpdater.Services.RedgeUpdateService.Enum;
using KanUpdater.Services.RedgeUpdateService.Models;
using KanUpdater.Services.RedgeUpdateService.NodeTypeFactory;
using System.Globalization;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using static Umbraco.Cms.Core.Constants.Conventions;

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
            target.ExternalId = source.AssignedContent.Id.ToString();
            target.ParentId = source.AssignedContent.Parent.Id.ToString();
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
                    Title = source.AssignedContent.Value<string>(childNodeType.TranslationTitle),
                    Description = source.AssignedContent.Value<string>(childNodeType.TranslationDescription)
                }
            };

            target.Genres = GetPublishedTags(childNodeType.GetGenreTagsCacheMap(source));
            target.Tags = GetPublishedTags(childNodeType.GetGeneralTagsCacheMap(source));
            target.AgeRestriction = GetPublishedTags(new Dictionary<string, IPublishedContent>() { { childNodeType.AgeRestriction, source.AssignedContent } })
                .GetAgeRestriction();

            target.Category = source.AssignedSubclass.Value<string>(childNodeType.Category);
            target.KlhCode = source.AssignedContent.Value<string>(childNodeType.KlhCode);
            target.ExternalCreated = source.AssignedContent.CreateDate.ToString("o", CultureInfo.InvariantCulture);
            target.ExternalModified = source.AssignedContent.UpdateDate.ToString("o", CultureInfo.InvariantCulture);
            target.Platforms = MapPlatforms(source.Root.ContentType.Alias);
        }

        private void Map(ContentMapModel source, RedgeUpdateRequestModel target, MapperContext context)
        {
            target.ExternalId = source.AssignedContent.Id.ToString();
            target.ParentId = source.AssignedContent.ParentId.ToString();
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
                    Title = source.AssignedContent.GetValue<string>(childNodeType.TranslationTitle),
                    Description = source.AssignedContent.GetValue<string>(childNodeType.TranslationDescription)
                }
            };

            target.Category = source.AssignedSubclass.GetValue<string>(childNodeType.Category);
            target.KlhCode = source.AssignedContent.GetValue<string>(childNodeType.KlhCode);
            target.ExternalCreated = source.AssignedContent.CreateDate.ToString("o", CultureInfo.InvariantCulture);
            target.ExternalModified = source.AssignedContent.UpdateDate.ToString("o", CultureInfo.InvariantCulture);

            target.Genres = GetContentTags(childNodeType.GetGenreTagsConetntMap(source));
            target.Tags = GetContentTags(childNodeType.GetGeneralTagsContentMap(source));
            target.AgeRestriction = GetContentTags(new Dictionary<string, IContent>() { { childNodeType.AgeRestriction, source.AssignedContent } })
                .GetAgeRestriction();
            target.Platforms = MapPlatforms(source.Root.ContentType.Alias);
        }

        private IEnumerable<string> GetPublishedTags(Dictionary<string, IPublishedContent> publishedTagsMap)
        {
            if (publishedTagsMap == null || !publishedTagsMap.Any())
            {
                return Enumerable.Empty<string>();
            }

            var res = new List<string>();

            foreach (var map in publishedTagsMap)
            {
                var tags = map.Value.Value<IEnumerable<IPublishedContent>>(map.Key);

                if (tags == null || !tags.Any())
                {
                    continue;
                }

                res.AddRange(tags.Select(x => x.Name));
            }

            return res;
        }

        private IEnumerable<string> GetContentTags(Dictionary<string, IContent> publishedTagsMap)
        {
            if (publishedTagsMap == null || !publishedTagsMap.Any())
            {
                return Enumerable.Empty<string>();
            }

            var res = new List<string>();

            foreach (var map in publishedTagsMap)
            {
                var tags = GetContentPickerData(map.Value, map.Key);

                if (tags == null || !tags.Any())
                {
                    continue;
                }

                res.AddRange(tags.Select(x => x.Name));
            }

            return res;
        }

        private IEnumerable<IPublishedContent> GetContentPickerData(IContent content, string pickerField)
        {
            var pickerData = content.GetValue<string>(pickerField);

            if (string.IsNullOrEmpty(pickerData))
            {
                return Enumerable.Empty<IPublishedContent>(); ;
            }

            return GetTagsAsPublishedContent(pickerData);
        }

        private IEnumerable<IPublishedContent> GetTagsAsPublishedContent(string pickerData)
        {
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

        private IEnumerable<RedgePlatform> MapPlatforms(string rootAlias)
        {
            var res = new List<RedgePlatform>() { RedgePlatform.SMART_TV, RedgePlatform.APPLE_TV, RedgePlatform.ANDROID_TV };

            switch (rootAlias)
            {
                case "homeKids":
                    res.AddRange(new[] { RedgePlatform.IOS_KIDS, RedgePlatform.ANDROID_KIDS });
                    break;

                default:
                    res.AddRange(new[] { RedgePlatform.IOS, RedgePlatform.ANDROID });
                    break;
            }

            return res;
        }
    }
}
