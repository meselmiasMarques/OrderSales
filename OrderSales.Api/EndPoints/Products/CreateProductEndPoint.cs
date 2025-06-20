using Microsoft.AspNetCore.Mvc;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Services;
using System.ComponentModel.DataAnnotations;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Responses;

namespace OrderSales.Api.EndPoints.Products
{
    public class CreateProductEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", ServiceAsync)
                .WithName("Products : Create")
                .WithSummary("Cadastra um novo Produto")
                .WithOrder(1)
                .Produces<Response<Product?>>();
        }

        private static async Task<IResult> ServiceAsync(
            IProductService ProductService,
            [FromBody] ProductCreateRequest request
        )
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
            {
                var errors = validationResults.Select(vr => vr.ErrorMessage);
                return Results.BadRequest(new { Errors = errors });
            }

            var result = await ProductService.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"{result.Data?.Id}", result.Data)
                : TypedResults.BadRequest(result.Data);
        }
    }
}
