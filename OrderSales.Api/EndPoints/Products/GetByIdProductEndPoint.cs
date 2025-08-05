using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Products
{
    public class GetByIdProductEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", ServiceAsync)
                .WithName("Products : GetById")
                .WithSummary("Lista  produto por ID")
                .WithOrder(4)
                .Produces<Response<Product?>>();
        }

        private static async Task<IResult> ServiceAsync(
            IProductService ProductService,
            Guid id
        )
        {
            var request = new ProductGetByIdRequest
            {
                Id = id
            };
            var result = await ProductService.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
