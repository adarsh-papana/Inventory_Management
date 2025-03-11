using DigitalBookstoreManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookstoreManagement.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);

          /*  modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Book)
                .WithOne(b => b.Inventory)
                .HasForeignKey<Inventory>(i => i.BookID)
                .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
