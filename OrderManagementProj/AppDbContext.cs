using static MiniOrderManagementSystemProj.EntityClasses;
using System.Data.Entity;

public class AppDbContext : DbContext
{
    public AppDbContext() : base("name=AppDbContext") { }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
}
