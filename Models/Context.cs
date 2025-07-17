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
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSchedule> EventSchedules { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }
        public DbSet<EventCard> EventCards { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }


    }
}
