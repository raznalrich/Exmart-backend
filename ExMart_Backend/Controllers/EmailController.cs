using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailRepository _mailRepository;
        private const int MaxSubjectLength = 998; 
        private const int MaxBodyLength = 10000; 

        public EmailController(IMailRepository mailRepository)
        {
            _mailRepository = mailRepository ?? throw new ArgumentNullException(nameof(mailRepository));
        }

        [AllowAnonymous]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmail(
            [Required(ErrorMessage = "Receptor email is required")]
            [EmailAddress(ErrorMessage = "Invalid email address format")]
            string receptor,

            [Required(ErrorMessage = "Subject is required")]
            [StringLength(MaxSubjectLength, ErrorMessage = "Subject is too long")]
            string subject,

            [Required(ErrorMessage = "Body is required")]
            [StringLength(MaxBodyLength, ErrorMessage = "Body exceeds maximum length")]
            string body)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Message = "Invalid request parameters",
                        Errors = ModelState
                    });
                }

                if (!IsValidEmail(receptor))
                {
                    return BadRequest(new
                    {
                        Message = "Invalid email format",
                        Email = receptor
                    });
                }

                if (string.IsNullOrWhiteSpace(subject?.Trim()))
                {
                    return BadRequest(new { Message = "Subject cannot be empty or whitespace" });
                }

                if (string.IsNullOrWhiteSpace(body?.Trim()))
                {
                    return BadRequest(new { Message = "Body cannot be empty or whitespace" });
                }

                if (ContainsSuspiciousContent(subject) || ContainsSuspiciousContent(body))
                {
                    return BadRequest(new { Message = "Message content appears to be suspicious" });
                }

                await _mailRepository.SendEmail(receptor.Trim(), subject.Trim(), body.Trim());

                return Ok(new
                {
                    Message = "Email sent successfully",
                    Recipient = receptor
                });
            }
            catch (InvalidOperationException ex)
            {
            
                return StatusCode(
                    StatusCodes.Status400BadRequest,
                    new { Message = "Failed to send email: " + ex.Message }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "An error occurred while sending the email" }
                );
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                if (!Regex.IsMatch(email, pattern))
                    return false;

                if (email.Length > 254) 
                    return false;

                var parts = email.Split('@');
                if (parts[0].Length > 64)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ContainsSuspiciousContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return false;

            var suspiciousPatterns = new[]
            {
                "<script",
                "javascript:",
                "vbscript:",
                "data:",
                "document.cookie",
                "onclick=",
                "onerror=",
                "onload=",
                "eval(",
                "execCommand(",
                "innerHTML"
            };

            return suspiciousPatterns.Any(pattern =>
                content.Contains(pattern, StringComparison.OrdinalIgnoreCase));
        }
    }
}