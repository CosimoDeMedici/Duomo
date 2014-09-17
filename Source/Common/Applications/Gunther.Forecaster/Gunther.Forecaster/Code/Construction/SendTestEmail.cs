using System;
using System.Net;
using System.Net.Mail;


namespace Duomo.Common.Gunther.Forecaster
{
    class SendTestEmail
    {
        public static void Test()
        {
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("duomo.automation@gmail.com", "Duomo Automation");
                //message.To.Add("vedika.khemani@gmail.com");
                //message.CC.Add("david.coats@gmail.com");
                message.To.Add("david.coats@gmail.com");

                // Basic text.
                message.Subject = "Test of line feeds.";
                message.Body = "This is line 1.\nThis is line 2.";
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
    }
}
