using DAL.Data;
using DAL.Interface;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<Student> students)
        {
            await _context.Students.AddRangeAsync(students);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<List<string>> GetAllStudentEmailsAsync()
        {
            return await _context.Students
                .Select(s => s.Email)
                .ToListAsync();
        }

    }

}
