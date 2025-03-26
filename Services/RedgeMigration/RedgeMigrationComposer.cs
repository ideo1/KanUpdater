using Umbraco.Cms.Core.Composing;

namespace KanUpdater.Services.RedgeMigration
{
    public class RedgeMigrationComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IRedgeMigrationService, RedgeMigrationService>();
            builder.Services.AddOptions<MigrationConfiguration>()
                 .Bind(builder.Config.GetSection(MigrationConfiguration.ConfigurationName));
        }
    }
}
