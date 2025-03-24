﻿using Quartz;
using System.Diagnostics;

namespace KanUpdater.Services.RedgeMigration.Jobs
{
    public class MigrateSubclasses : IJob
    {
        public static readonly JobKey Key = new JobKey("Migrate_Subclasses", "Migration");
        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("Job started");
            await Task.Delay(25000); // simulate a long-running task
            Debug.WriteLine("Job finished");
        }
    }
}
