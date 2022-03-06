using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzNetDailyAtHourAndMinute.Listeners
{
    using Quartz;

    public class JobExecutedEventArgs: EventArgs
    {
        public IJobDetail JobDetail { get; set; }
        public ITrigger Trigger { get; set; }

    }
}
