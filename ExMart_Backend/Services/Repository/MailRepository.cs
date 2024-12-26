using System.Net.Mail;
using System.Net;
using ExMart_Backend.Services.Interface;

namespace ExMart_Backend.Services.Repository
{
    public class MailRepository:IMailRepository
    {
        private readonly IConfiguration configuration;
        public MailRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(string recepter, string subject, string body)
        {
            var email = configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var password = configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var host = configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            var port = configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(email, password);

            var message = new MailMessage(email!, recepter, subject, body);
            await smtpClient.SendMailAsync(message);
        }
    }
}
