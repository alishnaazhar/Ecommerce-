using DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DL;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Coupon> Coupons { get; set; }



    //setting up the foreign key relationship.
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Product>()
    //        .HasOne(p => p.Category)
    //        .WithMany(c => c.Products)
    //        .HasForeignKey(p => p.CategoryId);
    //}

}
