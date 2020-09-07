using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendNewsletterEmail
{
    internal class Notify : IJob
    {

        Task IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                var email = context.MergedJobDataMap.GetString("email");
                SendEmail(email);
                ScheduleManager.Instance.Unschedule(new TriggerKey(email));
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.CompletedTask;
            }
        }

        private void SendEmail(string email)
        {
            Console.WriteLine("{0}: sending email to {1}...", DateTime.Now, email);
        }
    }
}
