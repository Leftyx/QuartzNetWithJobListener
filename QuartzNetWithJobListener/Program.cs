using System;

namespace QuartzNetDailyAtHourAndMinute
{
    using Quartz;
    using Quartz.Impl;
    using Quartz.Impl.Matchers;
    using QuartzNetDailyAtHourAndMinute.Listeners;
    using System.Collections.Generic;

    class Program
    {
        public static StdSchedulerFactory SchedulerFactory;
        public static IScheduler Scheduler;

        static void Main(string[] args)
        {

            SchedulerFactory = new StdSchedulerFactory();
            Scheduler = SchedulerFactory.GetScheduler();

            Scheduler.Start();

            Console.WriteLine("Quartz.Net Started ..."); 

            Test002(Scheduler);

            Console.ReadLine();
        }

        private static void Test002(IScheduler Scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                                       .WithIdentity("job1", "myJobGroup")
                                       .Build();

            ITrigger trigger = TriggerBuilder
                    .Create()
                    .WithSchedule(DailyTimeIntervalScheduleBuilder
                        .Create()
                        .OnMondayThroughFriday()
                        .WithIntervalInSeconds(10))
                    .StartNow()
                    .WithIdentity("trigger1", "myJobGroup")
                    .Build();

            Scheduler.ScheduleJob(job, trigger);

            var myJobListener = new MyJobListener();
            myJobListener.Name = "MyJobListener";
            myJobListener.JobExecuted += OnJobExecuted;

            Scheduler.ListenerManager.AddJobListener(myJobListener, KeyMatcher<JobKey>.KeyEquals(new JobKey("job1", "myJobGroup")));

            GetAllJobs(Scheduler);

        }

        private static void OnJobExecuted(object sender, JobExecutedEventArgs e)
        {
            string message = string.Format("JOB EXECUTED: Job Key:{0} - Trigger Key:{1}", e.JobDetail.Key, e.Trigger.Key);
            Console.WriteLine(message);
        }

        private static void GetAllJobs(IScheduler scheduler)
        {
            IList<string> jobGroups = scheduler.GetJobGroupNames();
            IList<string> triggerGroups = scheduler.GetTriggerGroupNames();

            foreach (string group in jobGroups)
            {
                var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                var jobKeys = scheduler.GetJobKeys(groupMatcher);
                foreach (var jobKey in jobKeys)
                {
                    var detail = scheduler.GetJobDetail(jobKey);
                    var triggers = scheduler.GetTriggersOfJob(jobKey);
                    foreach (ITrigger trigger in triggers)
                    {
                        Console.WriteLine(group);
                        Console.WriteLine(jobKey.Name);
                        Console.WriteLine(detail.Description);
                        Console.WriteLine(trigger.Key.Name);
                        Console.WriteLine(trigger.Key.Group);
                        Console.WriteLine(trigger.GetType().Name);
                        Console.WriteLine(scheduler.GetTriggerState(trigger.Key));
                        DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                        if (nextFireTime.HasValue)
                        {
                            Console.WriteLine(nextFireTime.Value.LocalDateTime.ToString());
                        }

                        DateTimeOffset? previousFireTime = trigger.GetPreviousFireTimeUtc();
                        if (previousFireTime.HasValue)
                        {
                            Console.WriteLine(previousFireTime.Value.LocalDateTime.ToString());
                        }
                    }
                }
            }
        }

    }
}
