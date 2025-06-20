namespace OrderSales.Core.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int Amount { get; set; }
    public decimal UnitPrice { get; set; }

    
}