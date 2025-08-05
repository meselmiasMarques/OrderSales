using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Customers
{
    public class GetByIdCustomerEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", ServiceAsync)
                .WithName("Customers : GetById")
                .WithSummary("Lista  cliente por ID")
                .WithOrder(3)
                .Produces<Response<Customer?>>();
        }

        private static async Task<IResult> ServiceAsync(
            ICustomerService CustomerService,
            Guid id
        )
        {
            var request = new CustomerGetByIdRequest
            {
                Id = id
            };
            var result = await CustomerService.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
