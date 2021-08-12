using GameStore.Email.Publisher.Models;
using GameStore.Email.Publisher.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameStore.Email.Publisher.Controllers
{
    [ApiController]
    [Route("[controller]/Send")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailService _emailService;

        public EmailController(
            ILogger<EmailController> logger,
            IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult PostMessage([FromBody] MessageModel message)
        {
            try
            {
                _emailService.SendMessage(message);

                return Accepted();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}