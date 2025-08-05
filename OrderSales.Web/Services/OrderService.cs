using OrderSales.Core.Models;
using OrderSales.Core.Requests.Orders;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;
using System.Net.Http.Json;

namespace OrderSales.Web.Services
{
    public class OrderService(IHttpClientFactory client) : IOrderService
    {
        private readonly HttpClient _client = client.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Order?>> CreateAsync(OrderCreateRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/orders", request);

            return await result.Content.ReadFromJsonAsync<Response<Order?>>()
                   ?? new Response<Order?>(null, 400, "Failed to create order");
        }

        public async Task<Response<Order?>> DeleteAsync(OrderDeleteRequest request)
        {
            var result = await _client.DeleteAsync($"v1/orders/{request.Id}");

            return await result.Content.ReadFromJsonAsync<Response<Order?>>()
                   ?? new Response<Order?>(null, 400, "Failed to delete order");
        }

        public async Task<Response<List<Order>?>> GetAllAsync(OrderGetAllRequest request)
            => await _client.GetFromJsonAsync<Response<List<Order>?>>("v1/orders") ??
                new Response<List<Order>?>(null, 400, "Failed to list orders");


        public async Task<Response<Order?>> GetByIdAsync(OrderGetByIdRequest request)
        {
            var result = await _client.GetFromJsonAsync<Response<Order?>>($"v1/orders/{request.Id}");
            return result ?? new Response<Order?>(null, 400, "Order not found");
        }

        public async Task<Response<List<Order>?>> GetOrderByCustomer(OrderGetByCustomerRequest request)
        {
            var result = await _client.GetFromJsonAsync<Response<List<Order>?>>($"v1/orders/customer/{request.CustomerId}");
            return result ?? new Response<List<Order>?>(null, 400, "Failed to retrieve orders for customer");
        }

        public async Task<Response<Order?>> UpdateAsync(OrderUpdateRequest request)
        {
          var result = await _client.PutAsJsonAsync($"v1/orders/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Order?>>()
                   ?? new Response<Order?>(null, 400, "Failed to update order");
        }
    }
}
