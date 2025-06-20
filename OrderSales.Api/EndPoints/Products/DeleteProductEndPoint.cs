using OrderSales.Core.Requests.Products;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Products
{
    public class DeleteProductEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", ServiceAsync)
                .WithName("Products : Delete")
                .WithSummary("Deleta produto por ID")
                .WithOrder(4)
                .Produces<string>();
        }
        private static async Task<IResult> ServiceAsync(
            IProductService ProductService,
            Guid id
        )
        {
            var request = new ProductDeleteRequest
            {
                Id = id
            };
            var result = await ProductService.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest(result.Data);
        }
    }
}
