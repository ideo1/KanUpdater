using Quartz;
using System.Diagnostics;

namespace KanUpdater.Services.RedgeMigration.Jobs
{
    public class MigrateSubclasses : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("Job started");
            await Task.Delay(5000); // simulate a long-running task
            Debug.WriteLine("Job finished");
        }
    }
}
