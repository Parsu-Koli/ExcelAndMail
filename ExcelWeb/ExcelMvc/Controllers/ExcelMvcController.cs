using ExcelMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MvcExcelFrontend.Controllers
{
    public class ExcelMvcController : Controller
    {
        private readonly HttpClient _client;

        public ExcelMvcController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("BackendApi");
        }

        // ----------------------------- Upload UI -----------------------------
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // ----------------------------- Upload Excel File -----------------------------
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Error = "Please select a valid Excel file.";
                return View();
            }

            var content = new MultipartFormDataContent();

            var fileStream = file.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);

            content.Add(fileContent, "file", file.FileName);

            var response = await _client.PostAsync("Excel/Upload", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }

            ViewBag.Error = "Upload failed.";
            return View();
        }

        // ----------------------------- Show Students Table -----------------------------
        public async Task<IActionResult> List()
        {
            var response = await _client.GetAsync("Excel/All");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load data.";
                return View(new List<Student>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var students = JsonSerializer.Deserialize<List<Student>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(students);
        }
    }
}
