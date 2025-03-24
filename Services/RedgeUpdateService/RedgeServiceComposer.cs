using KanUpdater.Services.RedgeUpdateService.NodeTypeFactory;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Mapping;

namespace KanUpdater.Services.RedgeUpdateService
{
    public class RedgeServiceComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IRedgeUpdateService, RedgeUpdateService>();
            builder.Services.AddTransient<ChildNodeTypeRedge, SubclassNodeTypeRedge>();
            builder.Services.AddTransient<ChildNodeTypeRedge, ProgramNodeTypeRedge>();
            builder.Services.AddTransient<IChildNodeTypeFactoryRedge, ChildNodeTypeFactoryRedge>();
            builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>().Add<RedgeMapper>();
        }
    }
}
