using BLL.DTOs;
using DAL.Interface;
using DAL.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailServices
    {
        private readonly IEmailRepository _emailRepo;
        private readonly IRecipientRepository _recipientRepo;
        private readonly EmailSettings _settings;

        public EmailServices(IEmailRepository emailRepo, IRecipientRepository recipientRepo, IOptions<EmailSettings> settings)
        {
            _emailRepo = emailRepo;
            _recipientRepo = recipientRepo;
            _settings = settings.Value;
        }

        public async Task<IEnumerable<Recipient>> GetAllRecipientsAsync()
        {
            return await _recipientRepo.GetAllRecipientsAsync();
        }

        public async Task<bool> SendEmailToManyAsync(List<string> emails, string subject, string body)
        {
            bool allSuccess = true;

            foreach (var toEmail in emails)
            {
                try
                {
                    using var smtp = new SmtpClient(_settings.Host, _settings.Port)
                    {
                        EnableSsl = true,
                        Credentials = new NetworkCredential(_settings.FromEmail, _settings.AppPassword)
                    };

                    var msg = new MailMessage(_settings.FromEmail, toEmail)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    await smtp.SendMailAsync(msg);

                    await _emailRepo.SaveEmailAsync(new Email
                    {
                        ToEmail = toEmail,
                        Subject = subject,
                        Body = body,
                        SentDate = System.DateTime.Now,
                        Status = "SENT"
                    });
                }
                catch
                {
                    allSuccess = false;
                    await _emailRepo.SaveEmailAsync(new Email
                    {
                        ToEmail = toEmail,
                        Subject = subject,
                        Body = body,
                        SentDate = System.DateTime.Now,
                        Status = "FAILED"
                    });
                }
            }

            return allSuccess;
        }

        public async Task<IEnumerable<Email>> GetAllEmailsAsync()
        {
            return await _emailRepo.GetAllEmailsAsync();
        }
    }
}
