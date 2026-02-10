using DAL.Models;


namespace DAL.Interface
{
    public interface IRecipientRepository
    {
        Task<IEnumerable<Recipient>> GetAllRecipientsAsync();
    }
}
