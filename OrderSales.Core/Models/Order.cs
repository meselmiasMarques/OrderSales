using OrderSales.Core.Models.Enums;

namespace OrderSales.Core.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<OrderItem> OrderItems { get; set; } = new();
    public decimal TotalValue { get; set; }

    public EStatusType StatusType { get; set; } = EStatusType.Created;
}