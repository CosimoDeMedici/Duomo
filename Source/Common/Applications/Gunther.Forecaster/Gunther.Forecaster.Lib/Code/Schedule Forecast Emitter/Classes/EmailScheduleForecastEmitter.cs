using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

using Duomo.Common.Gunther.Lib;


namespace Duomo.Common.Gunther.Forecaster.Lib
{
    public class EmailScheduleForecastEmitter : IScheduleForecastEmitter
    {
        #region Static

        private static void SendMail(string subject, string body)
        {
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("duomo.automation@gmail.com", "Duomo Automation");
                message.To.Add("david.coats@gmail.com");

                // Basic text.
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = false;

                using (SmtpClient smptClient = new SmtpClient())
                {
                    smptClient.Host = "smtp.gmail.com";
                    smptClient.Port = 587;
                    smptClient.EnableSsl = true;
                    smptClient.Timeout = 10000;
                    smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smptClient.UseDefaultCredentials = false;
                    smptClient.Credentials = new System.Net.NetworkCredential("duomo.automation@gmail.com", "Florence2012");

                    smptClient.Send(message);
                }
            }
        }

        #endregion

        #region IScheduleForecastEmitter Members

        public void EmitSchduleForecast(DateTime startDate, DateTime endDate, List<ScheduledTimeSpecification> scheduledTimes)
        {
            // Sections:
            //  1. Sorted by date.

            // Section 1: Sort by date.
            scheduledTimes.Sort(new SortByDate());

            StringBuilder builder = new StringBuilder();
            foreach (ScheduledTimeSpecification curTimeSpec in scheduledTimes)
            {
                string curLine = String.Format("{0:yyyyMMdd HH:mm:ss} - {1}", curTimeSpec.ScheduledTime, curTimeSpec.ScheduledJob.JobSpecification.ToString());
                builder.AppendLine(curLine);
            }

            string body = builder.ToString();
            string subject = String.Format("Schedule forecast for {0:yyyyMMdd HH:mm:ss} to {1:yyyyMMdd HH:mm:ss}", startDate, endDate);

            EmailScheduleForecastEmitter.SendMail(subject, body);
        }

        #endregion
    }


    class SortByDate : IComparer<ScheduledTimeSpecification>
    {
        #region IComparer<ScheduledTimeSpecification> Members

        public int Compare(ScheduledTimeSpecification x, ScheduledTimeSpecification y)
        {
            return x.ScheduledTime.CompareTo(y.ScheduledTime);
        }

        #endregion
    }
}
