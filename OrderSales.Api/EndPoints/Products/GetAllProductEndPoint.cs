using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Products
{
    public class GetAllProductEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", ServiceAsync)
                .WithName("Products : GetAll")
                .WithSummary("Lista todos produtos")
                .WithOrder(3)
                .Produces<Response<Product?>>();
        }

        private static async Task<IResult> ServiceAsync(
            IProductService ProductService
        )
        {
            var request = new ProductGetAllRequest();
            var result = await ProductService.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result.Data)
                : TypedResults.BadRequest(result.Data);
        }
    }
}
