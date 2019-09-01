using Microsoft.EntityFrameworkCore;

namespace EventApp.Models {
    public class EventAppDbContext : DbContext {
        
        public DbSet<Event> Events { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public EventAppDbContext(DbContextOptions<EventAppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Invitation>().HasKey(i => new { i.EventId, i.PersonId});

            base.OnModelCreating(modelBuilder);
        }

    }
}