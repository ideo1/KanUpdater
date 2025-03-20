using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Events;

namespace KanUpdater.Services.RedgeUpdateService
{
    public class RedgeServiceComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IRedgeUpdateService, RedgeUpdateService>();
        }
    }
}
