using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzNetDailyAtHourAndMinute.Listeners
{
    using Common.Logging;
    using Quartz;

    public class MyJobListener : IJobListener
    {
        ILog log = LogManager.GetCurrentClassLogger();

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            JobKey jobKey = context.JobDetail.Key;
            string message = string.Format("Job Key:{0} - Trigger Key:{1} - Start Time:{2} - Schedule Fire Time: {3}", context.JobDetail.Key, context.Trigger.Key, context.Trigger.StartTimeUtc, context.ScheduledFireTimeUtc);
            log.Debug(message);
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            JobKey jobKey = context.JobDetail.Key;
            string message = string.Format("Job Key:{0} - Trigger Key:{1} - Start Time:{2} - Schedule Fire Time: {3}", context.JobDetail.Key, context.Trigger.Key, context.Trigger.StartTimeUtc, context.ScheduledFireTimeUtc);
            log.Debug(message);
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            JobKey jobKey = context.JobDetail.Key;
            string message = string.Format("Job Key:{0} - Trigger Key:{1} - Start Time:{2} - Schedule Fire Time: {3}", context.JobDetail.Key, context.Trigger.Key, context.Trigger.StartTimeUtc, context.ScheduledFireTimeUtc);
            log.Debug(message);

            var e = new JobExecutedEventArgs();
            
            e.JobDetail = context.JobDetail;
            e.Trigger = context.Trigger;

            OnJobExecuted(e);
        }

        public string Name { get; set; }

        public event EventHandler<JobExecutedEventArgs> JobExecuted = null;

        protected virtual void OnJobExecuted(JobExecutedEventArgs e)
        {
            EventHandler<JobExecutedEventArgs> handler = JobExecuted;
            if (handler != null)
            {
                handler(this, e);
            }
        }


    }
}
