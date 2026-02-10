using DAL.Data;
using DAL.Interface;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RecipientRepository : IRecipientRepository
    {
        private readonly AppDbContext _context;

        public RecipientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipient>> GetAllRecipientsAsync()
        {
            return await _context.Recipients.ToListAsync();
        }
    }
}
