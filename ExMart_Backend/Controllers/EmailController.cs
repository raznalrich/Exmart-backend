using ExMart_Backend.Services.Interface;
//using ExMart_Backend.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController:ControllerBase
    {
        private readonly IMailRepository _mailRepository;
        public EmailController(IMailRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(string receptor, string subject, string body)
        {
            await _mailRepository.SendEmail(receptor, subject, body);
            return Ok();
        }
    }
}
