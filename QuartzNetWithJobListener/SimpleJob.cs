using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace QuartzNetDailyAtHourAndMinute
{
    using Quartz;

    public class SimpleJob : IJob
    {
        public virtual void Execute(IJobExecutionContext context)
        {
            JobKey jobKey = context.JobDetail.Key;

            string message = string.Format("Job Key:{0} - Trigger Key:{1} - Start Time:{2} - Schedule Fire Time: {3}", context.JobDetail.Key, context.Trigger.Key, context.Trigger.StartTimeUtc, context.ScheduledFireTimeUtc);

            ILog log = LogManager.GetCurrentClassLogger();
            log.Debug(message);

            Console.WriteLine("Trigger Info: " + message);
            Console.WriteLine("Next Schedule: " + context.Trigger.GetNextFireTimeUtc());
        }
    }
}
