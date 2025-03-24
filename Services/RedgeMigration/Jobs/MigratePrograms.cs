using Quartz;
using System.Diagnostics;

namespace KanUpdater.Services.RedgeMigration.Jobs
{
    public class MigratePrograms : IJob
    {
        public static readonly JobKey Key = new JobKey("Migrate_Programs", "Migration");
        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("Program started");
            await Task.Delay(25000); // simulate a long-running task
            Debug.WriteLine("Program finished");
        }
    }
}
