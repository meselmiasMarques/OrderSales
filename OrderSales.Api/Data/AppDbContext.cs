using Microsoft.EntityFrameworkCore;
using OrderSales.Core.Models;

namespace OrderSales.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region CUSTOMER

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR");
            
            entity.Property(x => x.Address)
                .IsRequired(true)
                .HasMaxLength(200)
                .HasColumnType("VARCHAR");
            
            entity.Property(x => x.Email)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR");
            
            entity.Property(x => x.Phone)
                .HasMaxLength(11)
                .HasColumnType("VARCHAR");
        });

        #endregion

        #region Product

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");
            
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR");
            
            entity.Property(x => x.Price)
                .IsRequired(true)
                .HasColumnType("MONEY");
            
            entity.Property(x => x.Stock)
                .IsRequired(true)
                .HasColumnType("SMALLINT");
        });

        #endregion

        #region ORDER

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order"); 
            
            entity.HasKey(x => x.Id);

            entity.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(x => x.CreatedAt)
                .IsRequired(true)
                .HasColumnType("DATETIME");
            
            entity.Property(x => x.TotalValue)
                .IsRequired(true)
                .HasColumnType("MONEY");
            
            entity.Property(o => o.StatusType)
                .HasConversion<string>() 
                .IsRequired();
                


        });

        #endregion
        
        #region OrderItem

        // OrderItem
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.Id);

            entity.Property(oi => oi.Amount)
                .IsRequired()
                .HasColumnType("INT");
            
            entity.Property(oi => oi.UnitPrice)
                .HasColumnType("DECIMAL");

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        });

        #endregion
    }
}