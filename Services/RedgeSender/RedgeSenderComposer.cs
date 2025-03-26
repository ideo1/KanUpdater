using KanUpdater.Services.RedgeUpdateService;
using Umbraco.Cms.Core.Composing;

namespace KanUpdater.Services.RedgeSender
{
    public class RedgeSenderComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            var redgeUpdateConfiguration = builder.Config.GetSection(RedgeUpdateConfiguration.ConfigurationName)
                .Get<RedgeUpdateConfiguration>();

            if (redgeUpdateConfiguration.UseStubSender)
            {
                builder.Services.AddTransient<IRedgeSender, RedgeSenderStub>();
            }
            else 
            {
                builder.Services.AddTransient<IRedgeSender, RedgeSender>();
            }

        }
    }
}
