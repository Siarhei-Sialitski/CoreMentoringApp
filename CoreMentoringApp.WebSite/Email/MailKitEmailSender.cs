using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Options;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CoreMentoringApp.WebSite.Email
{
    public class MailKitEmailSender : IEmailSender
    {

        private readonly MailKitOptions _mailKitOptions;

        public MailKitEmailSender(IOptions<MailKitOptions> optionsAccessor)
        {
            _mailKitOptions = optionsAccessor.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage()
            {
                Subject = subject,
                Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = htmlMessage
                }
            };

            emailMessage.From.Add(new MailboxAddress(_mailKitOptions.Name, _mailKitOptions.Address));
            emailMessage.To.Add(new MailboxAddress("", email));

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_mailKitOptions.Host, _mailKitOptions.Port, _mailKitOptions.UseSsl);
                await client.AuthenticateAsync(_mailKitOptions.SmtpUserName, _mailKitOptions.SmtpPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
