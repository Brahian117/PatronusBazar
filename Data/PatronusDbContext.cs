using Microsoft.EntityFrameworkCore;
using PatronusBazar.Models;

namespace PatronusBazar.Data
{
    public class PatronusDbContext : DbContext
    {
        public PatronusDbContext(DbContextOptions<PatronusDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
