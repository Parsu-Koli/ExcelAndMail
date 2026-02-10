using DAL.Models;
namespace DAL.Interface
{    
    public interface IStudentRepository
    {
        Task AddRangeAsync(List<Student> students);
        Task<List<Student>> GetAllAsync();
        Task<List<string>> GetAllStudentEmailsAsync();

    }

}
