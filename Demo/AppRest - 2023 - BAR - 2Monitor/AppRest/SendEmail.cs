using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;

namespace AppRest
{
    class SendEmail
    {

        public string folderSendReport;
        public string smtpHostServer;
        public string emailHeader;
        public string smtpHostPort;
        public string fromEmailAddress;
        public string fromEmailUserName;
        public string fromEmailPassword;
        public string toEmailAddress; // parameter
        public string ccEmailAddress;
        public string flagSSL;


        public string strDetailAdd; // parameter
        public string strHeaderAdd; // paramenter


        public SendEmail(string strHeaderAdd, string strDetailAdd)
        {
            this.folderSendReport = ConfigurationSettings.AppSettings["FolderSendReport"];
            this.smtpHostServer = ConfigurationSettings.AppSettings["SmtpHostServer"];
            this.emailHeader = ConfigurationSettings.AppSettings["EmailHeader"];
            this.smtpHostPort = ConfigurationSettings.AppSettings["SmtpHostPort"];
            this.fromEmailAddress = ConfigurationSettings.AppSettings["FromEmailAddress"];
            this.fromEmailUserName = ConfigurationSettings.AppSettings["FromEmailUserName"];
            this.fromEmailPassword = ConfigurationSettings.AppSettings["FromEmailPassword"];
            this.toEmailAddress = ConfigurationSettings.AppSettings["ToEmailAddress"];
            this.ccEmailAddress = ConfigurationSettings.AppSettings["CCEmailAddress"];
            this.flagSSL = ConfigurationSettings.AppSettings["FlagEnableSSL"];

            this.strDetailAdd = strDetailAdd;
            this.strHeaderAdd = strHeaderAdd;



        }

        public void SendGmail()
        {
            SmtpClient smtpClient = null;
            NetworkCredential credential = null;
            MailMessage message = null;

            try
            {
                // Console.WriteLine("1.Start Send Mail");

                smtpClient = new SmtpClient(smtpHostServer, Int32.Parse(smtpHostPort));
                credential = new NetworkCredential(fromEmailUserName, fromEmailPassword);

                if (this.flagSSL == "Y")
                    smtpClient.EnableSsl = true;
                else
                    smtpClient.EnableSsl = false;


                smtpClient.Credentials = credential;

                message = new MailMessage();
                message.From = new MailAddress(fromEmailAddress);  // From Email

                string[] toEmails = toEmailAddress.Split(',');  // To Email
                foreach (string toEmail in toEmails)
                    message.To.Add(toEmail);

                string[] ccEmails = ccEmailAddress.Split(',');  // CC Email
                foreach (string ccEmail in ccEmails)
                    message.CC.Add(ccEmail);

                message.Subject = emailHeader + strHeaderAdd;  // Subject Email
                //  message.Body = "\n Dear , All \n\n    I like I Cream Report : " + DateTime.Now.ToString("dd MMM yyyy HH:mm") + "\n\n Thank you \n"; // Boby Email

                message.Body = strDetailAdd;

                //string[] attachfilePaths = Directory.GetFiles(folderSendReport + DateTime.Now.ToString("yyyyMMdd"));
                //foreach (string attachfilePath in attachfilePaths)
                //    message.Attachments.Add(new Attachment(attachfilePath));

                //   Console.WriteLine("2.Attach File Mail : Success");

                smtpClient.Send(message);
                //   Console.WriteLine("3.Send Mail : Success");

            }
            catch (Exception e)
            {
                message.Body = "\n Dear , All \n\n   Error Send Email : " + e.Message + "\n   " + DateTime.Now.ToString("dd MMM yyyy HH:mm") + "\n\n Thank you \n"; // Boby Email
                smtpClient.Send(message);
                Console.WriteLine("--- Error Send Mail : " + e.Message);
            }
            finally
            {

                smtpClient = null;
                credential = null;
                message = null;
            }

        }






    }
}
