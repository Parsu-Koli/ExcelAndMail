using BLL.DTOs;
using DAL.Interface;
using DAL.Models;
namespace BLL.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task AddStudentsAsync(List<StudentDto> students)
        {
            var list = students.Select(s => new Student
            {
                Name = s.Name,
                Email = s.Email,
                Age = s.Age
            }).ToList();

            await _repo.AddRangeAsync(list);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
