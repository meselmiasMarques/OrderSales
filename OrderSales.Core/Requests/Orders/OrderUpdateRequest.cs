using OrderSales.Core.Models;
using OrderSales.Core.Models.Enums;
using OrderSales.Core.Requests.OrderItem;
using System.ComponentModel.DataAnnotations;

namespace OrderSales.Core.Requests.Orders
{
    public class OrderUpdateRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo id do cliente é obrigatório")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Selecione pelo menos 1 item para o pedido")]
        public List<OrderItemCreateRequest> OrderItems { get; set; } = new();

        public decimal TotalValue { get; set; }

        [Required(ErrorMessage = "O status do pedido é obrigatório : Processing = 1,\r\n    Completed = 2,\r\n    Cancelled = 3")]
        public EStatusType StatusType { get; set; } 

    }
}
