using KanUpdater.Services.RedgeUpdateService;
using Quartz;
using System.Diagnostics;

namespace KanUpdater.Services.RedgeMigration.Jobs
{
    public class MigrateSubclasses : IJob
    {
        private readonly IRedgeMigrationService _redgeMigrationService;
        private readonly IRedgeUpdateService _redgeUpdateService;
        public MigrateSubclasses(IRedgeMigrationService redgeMigrationService,
                                 IRedgeUpdateService redgeUpdateService)
        {
            _redgeMigrationService = redgeMigrationService;
            _redgeUpdateService = redgeUpdateService;
        }
        public static readonly JobKey Key = new JobKey("Migrate_Subclasses", "Migration");
        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("Job started");
            var channels = _redgeMigrationService.GetMigratedSubclassIds();

            if (channels == null)
            {
                return;
            }

            var res = _redgeUpdateService.GetRedgeUpdateModels(channels.Subclasses.Select(x => x.UmbracoId));

            await Task.Delay(25000); // simulate a long-running task
            Debug.WriteLine("Job finished");
        }
    }
}
