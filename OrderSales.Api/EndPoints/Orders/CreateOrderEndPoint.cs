using Microsoft.AspNetCore.Mvc;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Orders;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace OrderSales.Api.EndPoints.Orders
{
    public class CreateOrderEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", ServiceAsync)
            .WithName("CreateOrder")
            .WithTags("Orders")
            .WithSummary("Cadastra um novo pedido")
            .WithOrder(1)
            .Produces<Response<Order?>>();
        }


        public static async Task<IResult> ServiceAsync(
            IOrderService OrderService,
            [FromBody] OrderCreateRequest request
            )
        {

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
            {
                var errors = validationResults.Select(vr => vr.ErrorMessage);
                return Results.BadRequest(new { Errors = errors });
            }

            var result = await OrderService.CreateAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"{result.Data?.Id}", result.Data)
                : TypedResults.BadRequest(result.Data);
        }

    }
}
