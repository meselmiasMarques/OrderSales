using OrderSales.Core.Models;
using OrderSales.Core.Requests.Orders;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Responses;

namespace OrderSales.Core.Services
{
    public interface IOrderService
    {
        Task<Response<Order?>> CreateAsync(OrderCreateRequest request);
        Task<Response<Order?>> UpdateAsync(OrderUpdateRequest request);
        Task<Response<Order?>> GetByIdAsync(OrderGetByIdRequest request);
        Task<Response<Order?>> DeleteAsync(OrderDeleteRequest request);
        Task<Response<List<Order>?>> GetAllAsync(OrderGetAllRequest request);
        Task<Response<List<Order>?>> GetOrderByCustomer(OrderGetByCustomerRequest request);
    }
}
