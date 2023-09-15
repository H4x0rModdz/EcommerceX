using EcommerceAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<User>()
            //    .HasMany(u => u.Products)
            //    .WithOne(p => p.User)
            //    .HasForeignKey(p => p.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<User>()
            //    .HasMany(u => u.Transactions)
            //    .WithOne(t => t.User)
            //    .HasForeignKey(t => t.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            //builder.Entity<Transaction>()
            //    .Property(a => a.Amount)
            //    .HasColumnType("decimal(18, 2)");

            builder.Entity<User>()
                .Property(b => b.Balance)
                .HasColumnType("decimal(18, 2)");
        }
    }
}