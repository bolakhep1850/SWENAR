using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SWENAR.Models;

namespace SWENAR.Data
{
    public class SWENARDBContext : IdentityDbContext<User, Role, int>
    {
        public SWENARDBContext(
            DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual File Files { get; set; }
        public virtual FileData FileData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Attachments)
                .WithOne(a => a.Invoice)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attachment>()
              .HasOne(a => a.File)
              .WithOne(f => f.Attachment)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<File>()
            .HasOne(a => a.FileData)
            .WithOne(f => f.File)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasMany(e => e.Claims)
               .WithOne()
               .HasForeignKey(e => e.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
