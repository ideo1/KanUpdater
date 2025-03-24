using NPoco.RowMappers;
using Quartz;

namespace KanUpdater.Services.Extensions
{
    public static class QuartzExtensions
    {
        public static string QuartzMigrationGroupName = "Migration";
        public static async Task<bool> IsThisGroupAlreadyExecuted(this IScheduler scheduler, string group)
        {
            var currentJobs = await scheduler.GetCurrentlyExecutingJobs();

            if (currentJobs == null || !currentJobs.Any())
            {
                return false;
            }

            foreach (var currentJob in currentJobs)
            {
                if (currentJob.JobDetail.Key.Group.InvariantEquals(group))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
