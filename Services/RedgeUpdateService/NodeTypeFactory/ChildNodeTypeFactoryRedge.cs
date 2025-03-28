using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public interface IChildNodeTypeFactoryRedge
    {
        ChildNodeTypeRedge GetInstance(IContent content);
        ChildNodeTypeRedge GetInstance(IPublishedContent content);
    }

    public class ChildNodeTypeFactoryRedge : IChildNodeTypeFactoryRedge
    {
        private readonly IEnumerable<ChildNodeTypeRedge> _childNodeTypes;
        private readonly ILogger<ChildNodeTypeFactoryRedge> _logger;
        public ChildNodeTypeFactoryRedge(IEnumerable<ChildNodeTypeRedge> childNodeTypes,
                                         ILogger<ChildNodeTypeFactoryRedge> logger)
        {
            _childNodeTypes = childNodeTypes;
            _logger = logger;
        }

        public ChildNodeTypeRedge GetInstance(IContent content)
        {
            return GetInstance(content.ContentType.Alias);
        }

        public ChildNodeTypeRedge GetInstance(IPublishedContent content)
        {
            return GetInstance(content.ContentType.Alias);
        }

        private ChildNodeTypeRedge GetInstance(string alias)
        {
            var childNodeType = new ChildNodeTypeRedge();

            try
            {
                childNodeType = alias switch
                {
                    "program" => GetChildNodeType(typeof(ProgramNodeTypeRedge)),
                    "subClass" => GetChildNodeType(typeof(SubclassNodeTypeRedge)),
                    "seasonSubject" => GetChildNodeType(typeof(SeasonNodeTypeRedge)),
                    "videoItem" => GetChildNodeType(typeof(VideoNodeTypeRedge)),
                    _ => throw new InvalidOperationException()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Cant find ChildNodeType for type {0}", alias);
            }

            return childNodeType;
        }

        private ChildNodeTypeRedge GetChildNodeType(Type type) =>
         _childNodeTypes.FirstOrDefault(x => x.GetType() == type);
    }
}
