using DAL.Data;
using DAL.Interface;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _context;

        public EmailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveEmailAsync(Email email)
        {
            _context.Emails.Add(email);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Email>> GetAllEmailsAsync()
        {
            return await _context.Emails.ToListAsync();
        }
    }

}
