using Examine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.DependencyInjection;
using Umbraco.Cms.Infrastructure.Examine.DependencyInjection;
using static Umbraco.Cms.Core.Constants;
using System.Linq;
using Umbraco.Cms.Core.Cache;
using Microsoft.OpenApi.Models;

namespace KanUpdater;

public class Startup
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;

    public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
    {
        _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Carole Umbraco APIs", Version = "v1" });
        });
        services.AddUmbraco(_env, _config)
        .AddBackOffice()
        //.AddExamineIndexes()
        .AddComposers()
        .Build();

        services.AddControllers();
        //services.AddEndpointsApiExplorer();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseUmbraco();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carole Umbraco APIs v1"));
    }
}

public class GetPublishedUpdate : INotificationHandler<ContentPublishedNotification>
{

    public void Handle(ContentPublishedNotification notification)
    {
        var s = notification;
    }
}

public class GetUnPublishedUpdate : INotificationHandler<ContentUnpublishedNotification>
{
    public void Handle(ContentUnpublishedNotification notification)
    {
        var s = notification;
    }
}

public class ContentRefreshNot : INotificationHandler<ContentCacheRefresherNotification>
{
    private readonly ILogger<ContentRefreshNot> _logger;

    public ContentRefreshNot(ILogger<ContentRefreshNot> logger)
    {
        _logger = logger;
    }

    public void Handle(ContentCacheRefresherNotification notification)
    {
        var cacheItems = notification.MessageObject as ContentCacheRefresher.JsonPayload[];

        if (cacheItems == null || !cacheItems.Any())
        {
            return;
        }

        foreach (var item in cacheItems)
        {
            _logger.LogWarning("Update notification with id {0}", item.Id);
        }
    }
}

public class DisableNuCacheDatabaseComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        //var settings = new Umbraco.Cms.Infrastructure.PublishedCache.PublishedSnapshotServiceOptions
        //{
        //    IgnoreLocalDb = true
        //};

        //builder.SetServerRegistrar<SubscriberServerRoleAccessor>();
        //builder.Services.AddSingleton(settings);
        builder.Components().Append<IndexUpdater>();
        builder.Services.AddTransient<INotificationHandler<ContentPublishedNotification>, GetPublishedUpdate>();
        builder.Services.AddTransient<INotificationHandler<ContentCacheRefresherNotification>, ContentRefreshNot>();
        builder.Services.AddTransient<INotificationHandler<ContentUnpublishedNotification>, GetUnPublishedUpdate>();
    }
}

public class IndexUpdater : IComponent
{
    private readonly IExamineManager _examineManager;
    public IndexUpdater(IExamineManager examineManager)
    {
        _examineManager = examineManager;
    }
    public void Initialize()
    {
        if (!_examineManager.TryGetIndex(UmbracoIndexes.ExternalIndexName, out IIndex index))
        {
            throw new IndexOutOfRangeException($"No index found by name {UmbracoIndexes.ExternalIndexName}");
        }

        if (!(index is BaseIndexProvider indexProvider))
        {
            throw new InvalidCastException("Could not cast");
        }

        indexProvider.TransformingIndexValues += IndexProviderTransformingIndexValues;
    }

    private void IndexProviderTransformingIndexValues(object? sender, IndexingItemEventArgs args)
    {
        try
        {
            var s = args;


            if (args.ValueSet.ItemType == "tender")
            {
                args.Cancel = true;
            }

        }
        catch (Exception ex)
        {

            var m = ex.Message;
        }
    }

    public void Terminate()
    {
        throw new NotImplementedException();
    }
}

public class SubscriberServerRoleAccessor : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.Subscriber;
}
