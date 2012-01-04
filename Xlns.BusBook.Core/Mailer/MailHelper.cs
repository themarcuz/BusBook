using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Xlns.BusBook.Core.Mailer
{
    public class MailHelper
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //ToDo: refactoring
        public void SendChangedPasswordEmail(string to)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(ConfigurationManager.Configurator.Istance.mailFrom);
                mail.To.Add(new MailAddress(to));
                mail.Subject = ConfigurationManager.Configurator.Istance.mailSubject;
                mail.Body = ConfigurationManager.Configurator.Istance.mailBodyChangePassword;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = ConfigurationManager.Configurator.Istance.smtpPort;
                smtp.Host = ConfigurationManager.Configurator.Istance.smtpHost;
                smtp.EnableSsl = true;
                smtp.Send(mail);
                logger.Debug("Email inviata con successo all'indirizzo {0}", to);
            }
            catch
            {
                throw;
            }
        }

        public void SendResetPasswordEmail(string to, string newPassword)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(ConfigurationManager.Configurator.Istance.mailFrom);
                mail.To.Add(new MailAddress(to));
                mail.Subject = ConfigurationManager.Configurator.Istance.mailSubject;
                mail.Body = ConfigurationManager.Configurator.Istance.mailBodyResetPassword
                    .Replace("{0}",to)
                    .Replace("{1}",newPassword);
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.Configurator.Istance.smtpUsername,
                     ConfigurationManager.Configurator.Istance.smtpPassword);
                smtp.Port = ConfigurationManager.Configurator.Istance.smtpPort;
                smtp.Host = ConfigurationManager.Configurator.Istance.smtpHost;
                smtp.EnableSsl = true;
                smtp.Send(mail);
                logger.Debug("Email di reset inviata con successo all'indirizzo {0} con password {1}", to, newPassword);
            }
            catch
            {
                throw;
            }
        }
    }
}
