using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzNetDailyAtHourAndMinute.Listeners
{
    using Quartz;

    public class MyTriggerListener : ITriggerListener
    {

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public void TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode)
        {
            throw new NotImplementedException();
        }

        public void TriggerFired(ITrigger trigger, IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public void TriggerMisfired(ITrigger trigger)
        {
            throw new NotImplementedException();
        }

        public bool VetoJobExecution(ITrigger trigger, IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
