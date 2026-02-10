using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BackendExcel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly StudentService _service;

        public ExcelController(StudentService service)
        {
            _service = service;
        }
                 
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            List<StudentDto> students = new();

            // ✔ Correct EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    if (worksheet == null)
                        return BadRequest("No worksheet found in the Excel file.");

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        students.Add(new StudentDto
                        {
                            Name = worksheet.Cells[row, 1]?.Text?.Trim(),
                            Email = worksheet.Cells[row, 2]?.Text?.Trim(),
                            Age = int.TryParse(worksheet.Cells[row, 3]?.Text, out int age) ? age : 0
                        });
                    }
                }
            }

            await _service.AddStudentsAsync(students);

            return Ok(new { message = "File uploaded successfully!" });
        }

        [HttpGet("All")]
        public async Task<IActionResult> All()
        {
            var data = await _service.GetStudentsAsync();
            return Ok(data);
        }
    }
}
