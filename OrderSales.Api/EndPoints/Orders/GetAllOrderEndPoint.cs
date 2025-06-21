

using Microsoft.AspNetCore.Mvc;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Orders;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Orders
{
    public class GetAllOrderEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", ServiceAsync)
                .WithName("GetAllOrders")
                .WithTags("Orders")
                .WithSummary("Obtém todos os pedidos")
                .WithOrder(2)
                .Produces<Response<List<Order>?>>();
        }

        private static async Task<IResult> ServiceAsync(
           IOrderService OrderService

            )
        {
            var request = new OrderGetAllRequest();
            var result = await OrderService.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result.Data)
                : TypedResults.BadRequest(result.Message);
        }
    }
}
