using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace Sciencecom.Models.Scheduler
{
    public class Scheduler
    {
        public static void Start()
        {
            IScheduler t = StdSchedulerFactory.GetDefaultScheduler();
            t.Start();

            IJobDetail job = JobBuilder.Create<Job>().Build();
            ITrigger trigger = TriggerBuilder.Create() 
                .WithIdentity("trigger1", "group1") 
                .StartAt(DateBuilder.DateOf(00, 01, 0))
                .WithSimpleSchedule(x => x 
                    .WithIntervalInHours(24) 
                    .RepeatForever()) 
                .Build();
            t.ScheduleJob(job, trigger);
        }
    }
}