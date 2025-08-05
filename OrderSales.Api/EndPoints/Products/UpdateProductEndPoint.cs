using Microsoft.AspNetCore.Mvc;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Services;
using System.ComponentModel.DataAnnotations;
using OrderSales.Core.Responses;

namespace OrderSales.Api.EndPoints.Products
{
    public class UpdateProductEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", ServiceAsync)
                .WithName("Products : Update")
                .WithSummary("Atualiza um Produto")
                .WithOrder(2)
                .Produces<Response<Product?>>();
        }

        private static async Task<IResult> ServiceAsync(
            IProductService ProductService,
            [FromBody] ProductUpdateRequest request
            ,Guid id
        )
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(request);

            if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
            {
                var errors = validationResults.Select(vr => vr.ErrorMessage);
                return Results.BadRequest(new { Errors = errors });
            }

            request.Id = id; 

            var result = await ProductService.UpdateAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
