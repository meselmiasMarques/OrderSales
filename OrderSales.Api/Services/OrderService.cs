using Microsoft.EntityFrameworkCore;
using OrderSales.Api.Data;
using OrderSales.Core.Models;
using OrderSales.Core.Models.Enums;
using OrderSales.Core.Requests.Orders;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.Services
{
    public class OrderService(AppDbContext context) : IOrderService
    {
        private readonly AppDbContext _context = context;

        #region CRIA PEDIDOS 
        public async Task<Response<Order?>> CreateAsync(OrderCreateRequest request)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId);
                if (customer is null)
                    return new Response<Order?>(null, 404, "Customer not found");

                var order = new Order
                {
                    CreatedAt = DateTime.UtcNow,
                    CustomerId = customer.Id,
                    StatusType = EStatusType.Created,
                    TotalValue = 0m, // Initialize TotalValue to 0
                };

                foreach (var item in request.OrderItems)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);

                    if (product is null)
                        return new Response<Order?>(null, 404, $"Product with ID {item.ProductId} not found");

                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Amount = item.Amount,
                        UnitPrice = product.Price
                    });

                    order.TotalValue = order.OrderItems.Sum(i => i.UnitPrice * i.Amount);

                }
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return new Response<Order?>(order, 201, "Order created successfully");

            }
            catch (Exception ex)
            {
                return new Response<Order?>(null, 400, $"Ocorreu um erro ao criar pedido - {ex.Message}");
            }

        }

        #endregion

        #region EXCLUI PEDIDOS

        public async Task<Response<Order?>> DeleteAsync(OrderDeleteRequest request)
        {

            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.Id);

                if (order == null)
                {
                    return new Response<Order?>(null, 404, "Order not found");
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                return new Response<Order?>(null, 200, "Order deleted successfully");
            }
            catch (Exception ex)
            {
                return new Response<Order?>(null, 400, $"Erro ao Excluir Pedido - {ex.Message}");
            }
        }

        #endregion

        #region LISTA PEDIDOS 

        public async Task<Response<List<Order>?>> GetAllAsync(OrderGetAllRequest request)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .ToListAsync();
                return new Response<List<Order>?>(orders, 200, "Orders retrieved successfully");
            }
            catch (Exception ex)
            {
                return new Response<List<Order>?>(null, 500, $"An error occurred while retrieving orders - {ex.Message}");
            }
        }


        #endregion

        #region RECUPERA PEDIDO PELO ID
        public async Task<Response<Order?>> GetByIdAsync(OrderGetByIdRequest request)
        {
            try
            {
                var order = await _context.Orders
              .Include(o => o.Customer)
              .Include(o => o.OrderItems)
              .ThenInclude(oi => oi.Product)
              .FirstOrDefaultAsync(o => o.Id == request.Id);

                return order == null
                    ? new Response<Order?>(null, 404, "Order not found")
                    : new Response<Order?>(order, 200, "Order retrieved successfully");
            }

            catch (Exception ex)
            {
                return new Response<Order?>(null, 500, $"An error occurred while updating the order - {ex.Message}");
            }
        }

        #endregion

        #region RECUPERA PEDIDO POR CLIENTE

        public async Task<Response<List<Order>?>> GetOrderByCustomer(OrderGetByCustomerRequest request)
        {
            try
            {
                var orders = await _context.Orders
                .Where(o => o.CustomerId == request.CustomerId)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

                return orders == null || !orders.Any()
                    ? new Response<List<Order>?>(null, 404, "No orders found for this customer")
                    : new Response<List<Order>?>(orders, 200, "Orders retrieved successfully for customer");

            }

            catch (Exception ex)
            {
                return new Response<List<Order>?>(null, 500, $"An error occurred while updating the order - {ex.Message}");
            }
        }


        #endregion

        #region ATUALIZA PEDIDOS

        public async Task<Response<Order?>> UpdateAsync(OrderUpdateRequest request)
        {
            return null;
        }


        #endregion

    }
}
