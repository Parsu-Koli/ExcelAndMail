using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendExcel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailServices _emailService;

        public EmailController(EmailServices emailService)
        {
            _emailService = emailService;
        }

        // GET all recipients (return List<string> of emails)
        [HttpGet("all-recipients")]
        public async Task<IActionResult> GetAllRecipients()
        {
            var recipients = await _emailService.GetAllRecipientsAsync();
            var emails = recipients.Select(r => r.Email).ToList(); // map Recipient to string
            return Ok(emails);
        }

        // POST send multiple emails
        [HttpPost("send-multiple")]
        public async Task<IActionResult> SendMultipleEmails([FromBody] EmailRequestDTO request)
        {
            if (request.ToEmails == null || request.ToEmails.Count == 0)
                return BadRequest(new { success = false, message = "No recipients selected." });

            bool result = await _emailService.SendEmailToManyAsync(request.ToEmails, request.Subject, request.Body);
            return Ok(new { success = result });
        }

        // GET all sent emails
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmails()
        {
            var emails = await _emailService.GetAllEmailsAsync();
            return Ok(emails);
        }
    }
}
