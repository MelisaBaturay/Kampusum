using Microsoft.EntityFrameworkCore;

namespace Kampusum.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
    : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<News> Newss { get; set; }
    }
}
