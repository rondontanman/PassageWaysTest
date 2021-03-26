using Microsoft.EntityFrameworkCore;

namespace backend {
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }
    }
}