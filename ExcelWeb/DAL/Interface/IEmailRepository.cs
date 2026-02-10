using DAL.Models;
namespace DAL.Interface
{
    public interface IEmailRepository
    {
        Task<int> SaveEmailAsync(Email email);
        Task<IEnumerable<Email>> GetAllEmailsAsync();
    }

}
