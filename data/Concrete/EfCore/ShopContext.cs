using entity;
using Microsoft.EntityFrameworkCore;

namespace data.Concrete.EfCore
{
    public class ShopContext:DbContext
    {
        public DbSet<Product> Products { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = shopDb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasKey(c=> new {c.ProductId});
        }
    }
}