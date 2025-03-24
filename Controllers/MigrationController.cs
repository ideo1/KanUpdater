using KanUpdater.Services.RedgeMigration.Jobs;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace KanUpdater.Controllers
{
    [Route("api/migration")]
    public class MigrationController : ControllerBase
    {
        private readonly ISchedulerFactory _schedulerFactory;

        public MigrationController(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        [HttpGet]
        [Route("subclasses")]
        public async Task<IActionResult> MigrateSubclasses(CancellationToken ct)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var job = JobBuilder.Create<MigrateSubclasses>()
                        .WithIdentity("name", "group")
                        .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("name", "group")
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger, ct);

            return Ok();
        }
    }
}
