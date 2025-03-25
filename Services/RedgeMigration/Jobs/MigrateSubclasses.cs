using Quartz;
using System.Diagnostics;

namespace KanUpdater.Services.RedgeMigration.Jobs
{
    public class MigrateSubclasses : IJob
    {
        private readonly IRedgeMigrationService _redgeMigrationService;
        public MigrateSubclasses(IRedgeMigrationService redgeMigrationService)
        {
            _redgeMigrationService = redgeMigrationService;
        }
        public static readonly JobKey Key = new JobKey("Migrate_Subclasses", "Migration");
        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("Job started");
            var channels = _redgeMigrationService.GetMigratedSubclassIds();
            await Task.Delay(25000); // simulate a long-running task
            Debug.WriteLine("Job finished");
        }
    }
}
