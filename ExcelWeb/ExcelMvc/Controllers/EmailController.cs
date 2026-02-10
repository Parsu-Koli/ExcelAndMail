using ExcelMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ExcelMvc.Controllers
{
    public class EmailController : Controller
    {
        private readonly HttpClient _client;

        public EmailController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("BackendApi");
        }

        // Load Send Email Page
        public async Task<IActionResult> Send()
        {
            var emails = await _client.GetFromJsonAsync<List<string>>("Email/all-recipients");
            ViewBag.AllEmails = emails;
            return View();
        }

        // Send Bulk Email
        [HttpPost]
        public async Task<IActionResult> Send(Email model)
        {
            var payload = new
            {
                ToEmails = model.SelectedEmails,
                model.Subject,
                model.Body
            };

            var response = await _client.PostAsJsonAsync("Email/send-multiple", payload);

            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, bool>>();

            if (response.IsSuccessStatusCode && result != null && result.ContainsKey("success") && result["success"])
            {
                TempData["msg"] = "Email sent successfully!";
                return RedirectToAction("AllSentEmails");
            }

            TempData["msg"] = "Failed to send emails!";
            return View(model);
        }

        // View All Sent Emails
        public async Task<IActionResult> AllSentEmails()
        {
            var emails = await _client.GetFromJsonAsync<List<Email>>("Email/all");
            return View(emails);
        }
    }
}
