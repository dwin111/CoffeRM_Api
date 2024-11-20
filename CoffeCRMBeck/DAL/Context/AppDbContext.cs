using CoffeCRMBeck.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeCRMBeck.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductCatalog>()
        //          .HasKey(m => new { m.Id, m.Name });
            
        //}
    }
}
