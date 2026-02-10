using DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
    }
}
