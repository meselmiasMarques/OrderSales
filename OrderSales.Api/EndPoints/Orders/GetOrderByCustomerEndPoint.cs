
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Orders;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Orders
{
    public class GetOrderByCustomerEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/customer/{customerId}", ServiceAsync)
                .WithName("Recupera Pedidos por cliente")
                .WithSummary("Recupera Pedidos por cliente")
                .WithDescription("Retrieves an order by its unique identifier.")
                .WithOrder(4)
                .Produces<Response<Order>>()
                .WithTags("Orders");
        }

        private static async Task<IResult> ServiceAsync(
            IOrderService OrderService,
            Guid customerId
            )
        {
            var request = new OrderGetByCustomerRequest { CustomerId = customerId };
            var result = await OrderService.GetOrderByCustomer(request);

            return result.IsSuccess
                ? Results.Ok(result.Data)
                : Results.BadRequest(result.Message);
        }
    }
}
