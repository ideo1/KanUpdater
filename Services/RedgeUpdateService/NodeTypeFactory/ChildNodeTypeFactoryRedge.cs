using Umbraco.Cms.Core.Models;

namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public interface IChildNodeTypeFactoryRedge
    {
        ChildNodeTypeRedge GetInstance(IContent content);
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
            var childNodeType = new ChildNodeTypeRedge();

            try
            {
                childNodeType = content.ContentType.Alias switch
                {
                    "program" => GetChildNodeType(typeof(ProgramNodeTypeRedge)),
                    "subClass" => GetChildNodeType(typeof(SubclassNodeTypeRedge)),
                    _ => throw new InvalidOperationException()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Cant find ChildNodeType for type {0}", content.ContentType.Alias);
            }

            return childNodeType;
        }

        private ChildNodeTypeRedge GetChildNodeType(Type type) =>
         _childNodeTypes.FirstOrDefault(x => x.GetType() == type);
    }
}
