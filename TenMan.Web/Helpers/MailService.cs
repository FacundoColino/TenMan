using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace TenMan.Web.Helpers
{
    public class MailService
    {
        IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void SendEmailGmail(String receptor, String asunto, String mensaje, string file)
        {
            MailMessage mail = new MailMessage();

            String usermail = this.configuration["usuariogmail"];
            String passwordmail = this.configuration["passwordgmail"];
            String token = this.configuration["token"];

            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            if (!string.IsNullOrEmpty(file))
            {
                Attachment attachment = new Attachment(file);
                mail.Attachments.Add(attachment);
            }

            String smtpserver = this.configuration["hostGmail"];
            int port = int.Parse(this.configuration["portGmail"]);
            bool ssl = bool.Parse(this.configuration["sslGmail"]);
            bool defaultcreadentials = bool.Parse(this.configuration["defaultcredentialsGmail"]);


            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = smtpserver;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = defaultcreadentials;
            smtpClient.Credentials = new NetworkCredential(usermail, token);

            smtpClient.Send(mail);
        }
        public void SendEmailOutlook(String receptor, String asunto, String mensaje)
        {
            MailMessage mail = new MailMessage();

            String usermail = this.configuration["usuariooutlook"];
            String passwordmail = this.configuration["passwordoutlook"];

            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            String smtpserver = this.configuration["host"];
            int port = int.Parse(this.configuration["port"]);
            bool ssl = bool.Parse(this.configuration["ssl"]);
            bool defaultcreadentials = bool.Parse(this.configuration["defaultcredentials"]);

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = smtpserver;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = defaultcreadentials;


            NetworkCredential usercredential = new NetworkCredential(usermail, passwordmail);

            smtpClient.Credentials = usercredential;
            smtpClient.Send(mail);
        }
    }
}
