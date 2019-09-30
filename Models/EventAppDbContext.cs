using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventApp.Models {
    public class EventAppDbContext : DbContext {
        
        public DbSet<Event> Events { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public EventAppDbContext(DbContextOptions<EventAppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            modelBuilder.Entity<Invitation>().HasKey(i => new { i.EventId, i.PersonId});

            modelBuilder.Entity<Person>().HasIndex(p => new {p.Name, p.DateOfBirth});
            modelBuilder.Entity<Event>().HasOne(e => e.Place).WithMany(p => p.Events).HasForeignKey(e => e.PlaceIdentity);

            //modelBuilder.Entity<Person>().HasMany(p => p.Friends).WithOne(p => p).HasForeignKey(p => p.Id);
            modelBuilder.Entity<Friend>().HasKey(f => new {f.Id});
            modelBuilder.Entity<Friend>().HasOne(f => f.Person).WithMany(p => p.Friends).HasForeignKey(f => f.PersonId).OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<EventStaff>().HasKey(es => new { es.EventId, es.OrganizerId });
            modelBuilder.Entity<EventStaff>().HasOne(es => es.Event).WithMany(e => e.Staff).HasForeignKey(es => es.EventId);
            modelBuilder.Entity<EventStaff>().HasOne(es => es.Organizer).WithMany(o => o.Events).HasForeignKey(es => es.OrganizerId);
            
            modelBuilder.RemoveOneToManyCascadeDeleteConvention();
            base.OnModelCreating(modelBuilder);
        }
    }

    public static class ModelBuilderExtensions
    {        
        public static void RemoveOneToManyCascadeDeleteConvention(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}