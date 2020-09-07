using EventCoApp.DataAccessLibrary.DataAccess;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendNewsletterEmail
{
    internal class Setup : IJob
    {
        private EventCoContext _context = new EventCoContext();

        Task IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                foreach (var user in _context.Users)
                {
                    if (user.IsSubscribed == true)
                    {
                        var email = user.Email;
                        var notify = new JobDetailImpl(email, "emailgroup", typeof(Notify))
                        {
                            JobDataMap = new JobDataMap { { "email", email } }
                        };
                        var time = new DateTimeOffset(DateTime.Now);
                        var trigger = new SimpleTriggerImpl(email, "emailtriggergroup", time);
                        ScheduleManager.Instance.Schedule(notify, trigger);
                    }
                }
                Console.WriteLine("{0}: all jobs scheduled for today", DateTime.Now);

                return Task.CompletedTask;
            }
            catch (Exception e) { return Task.CompletedTask; }
        }
    }
}

