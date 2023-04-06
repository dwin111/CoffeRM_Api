using System;
using MimeKit;

namespace CoffeCRMBeck.Service
{
    public class MailService
    {
        private readonly ILogger<MailService> _logger;

        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;

        }

        public void SenEmail(string emailTo, string emailFrom, string nameEmailFrom, string subject, string htmlBody)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(nameEmailFrom, emailFrom));
                message.To.Add(new MailboxAddress("User", emailTo));
                message.Subject = subject;
                message.Body = new BodyBuilder()
                { HtmlBody = htmlBody }.ToMessageBody();
                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(Configure.SMTP_SERVICE, Configure.SMTP_PORT, true);
                    client.Authenticate(Configure.MAIL_BOT, Configure.PASS_MAIL_BOT);
                    client.Send(message);

                    client.Disconnect(true);
                    _logger.LogInformation("Email send Good");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message);
            }
        }
    }
}

