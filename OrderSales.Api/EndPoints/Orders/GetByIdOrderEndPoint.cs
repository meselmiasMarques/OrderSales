

using OrderSales.Core.Models;
using OrderSales.Core.Requests.Orders;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Orders
{
    public class GetByIdOrderEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", ServiceAsync)
                .WithName("GetByIdOrder")
                .WithSummary("Get Order by Id")
                .WithDescription("Retrieves an order by its unique identifier.")
                .WithOrder(3)
                .Produces<Response<Order>>()
                .WithTags("Orders");
        }

        private static async Task<IResult> ServiceAsync(
            IOrderService OrderService,
            Guid id
            )
        {
            var request = new OrderGetByIdRequest { Id = id };
            var result = await OrderService.GetByIdAsync(request);

            return result.IsSuccess
                ? Results.Ok(result.Data)
                : Results.BadRequest(result.Message);
        }
    }
}
