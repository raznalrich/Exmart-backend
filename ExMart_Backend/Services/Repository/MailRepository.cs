using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using ExMart_Backend.Services.Interface;

namespace ExMart_Backend.Services.Repository
{
    public class MailRepository : IMailRepository, IDisposable
    {
        private readonly IConfiguration _configuration;
        private bool _disposed;
        private readonly string? _email;
        private readonly string? _password;
        private readonly string? _host;
        private readonly int _port;

        public MailRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // Initialize configuration values in constructor
            //_email = _configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            _email = Environment.GetEnvironmentVariable("EMAIL");
            _password = Environment.GetEnvironmentVariable("PASSWORD");
            //_password = _configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            _host = _configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            _port = _configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

            ValidateConfiguration();
        }

        public async Task SendEmail(string receiver, string subject, string body)
        {
            ValidateDisposed();
            ValidateEmailParameters(receiver, subject, body);

            SmtpClient? smtpClient = null;
            MailMessage? message = null;

            try
            {
                smtpClient = CreateSmtpClient();
                message = CreateMailMessage(receiver, subject, body);

                await smtpClient.SendMailAsync(message).ConfigureAwait(false);
            }
            catch (SmtpException ex)
            {
                // Log SMTP-specific errors
                throw new InvalidOperationException($"Failed to send email: {ex.StatusCode}", ex);
            }
            catch (Exception ex)
            {
                // Log general errors
                throw new InvalidOperationException("An error occurred while sending the email", ex);
            }
            finally
            {
                // Dispose of resources
                message?.Dispose();
                smtpClient?.Dispose();
            }
        }

        private void ValidateConfiguration()
        {
            var missingConfigs = new List<string>();

            if (string.IsNullOrWhiteSpace(_email))
                missingConfigs.Add("EMAIL");
            if (string.IsNullOrWhiteSpace(_password))
                missingConfigs.Add("PASSWORD");
            if (string.IsNullOrWhiteSpace(_host))
                missingConfigs.Add("HOST");
            if (_port <= 0)
                missingConfigs.Add("PORT");

            if (missingConfigs.Any())
            {
                throw new InvalidOperationException(
                    $"Missing or invalid email configuration(s): {string.Join(", ", missingConfigs)}"
                );
            }
        }

        private void ValidateEmailParameters(string receiver, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(receiver))
                throw new ArgumentException("Receiver email cannot be null or empty", nameof(receiver));

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject cannot be null or empty", nameof(subject));

            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("Body cannot be null or empty", nameof(body));

            if (!IsValidEmail(receiver))
                throw new ArgumentException("Invalid receiver email format", nameof(receiver));

            if (subject.Length > 998) // RFC 2822 subject length limit
                throw new ArgumentException("Subject exceeds maximum length of 998 characters", nameof(subject));
        }

        private SmtpClient CreateSmtpClient()
        {
            var smtpClient = new SmtpClient(_host, _port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_email, _password),
                Timeout = 30000 // 30 seconds timeout
            };

            return smtpClient;
        }

        private MailMessage CreateMailMessage(string receiver, string subject, string body)
        {
            var message = new MailMessage(_email!, receiver, subject, body)
            {
                IsBodyHtml = true
            };

            return message;
        }

        private bool IsValidEmail(string email)
        {
            // Email validation regex pattern
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private void ValidateDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MailRepository));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose of managed resources if any
                }
                _disposed = true;
            }
        }

        ~MailRepository()
        {
            Dispose(false);
        }
    }
}