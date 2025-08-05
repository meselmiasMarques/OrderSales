using OrderSales.Core.Models;
using OrderSales.Core.Requests.OrderItem;
using System.ComponentModel.DataAnnotations;

namespace OrderSales.Core.Requests.Orders;

public class OrderCreateRequest
{
    [Required(ErrorMessage = "Campo id do cliente é obrigatório")]
    public Guid CustomerId { get; set; }

    public Customer? Customer { get; set; } = null!;

    [Required(ErrorMessage = "Selecione pelo menos 1 item para o pedido")]
    public List<OrderItemCreateRequest> OrderItems { get; set; } = new();

    public List<Product> Products { get; set; } = new();
}
