using KanUpdater.Services.RedgeSender;
using KanUpdater.Services.RedgeUpdateService;
using Quartz;
using System.Diagnostics;

namespace KanUpdater.Services.RedgeMigration.Jobs
{
    public class MigrateSubclasses : IJob
    {
        private readonly IRedgeMigrationService _redgeMigrationService;
        private readonly IRedgeUpdateService _redgeUpdateService;
        private readonly IRedgeSender _redgeSender;
        public MigrateSubclasses(IRedgeMigrationService redgeMigrationService,
                                 IRedgeUpdateService redgeUpdateService,
                                 IRedgeSender redgeSender)
        {
            _redgeMigrationService = redgeMigrationService;
            _redgeUpdateService = redgeUpdateService;
            _redgeSender = redgeSender;
        }
        public static readonly JobKey Key = new JobKey("Migrate_Subclasses", "Migration");
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Debug.WriteLine("Job started");
                var channels = _redgeMigrationService.GetMigratedSubclassIds();

                if (channels == null)
                {
                    return;
                }

                var res = _redgeUpdateService.GetContentBasedRedgeUpdateModels(channels.Subclasses.Select(x => x.UmbracoId));
                await _redgeSender.SendDataAsync(res);

                await Task.Delay(25000); // simulate a long-running task
                Debug.WriteLine("Job finished");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
