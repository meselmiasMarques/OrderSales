using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Customers
{
    public class DeleteCustomerEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", ServiceAsync)
                .WithName("Customers : Delete")
                .WithSummary("Exclui um cliente")
                .WithOrder(4)
                .Produces<Response<Customer?>>();
        }

        private static async Task<IResult> ServiceAsync(
            ICustomerService CustomerService,
            Guid id
        )
        {
            var request = new CustomerDeleteRequest
            {
                Id = id
            };
            var result = await CustomerService.DeleteAsync(request);

            return result.IsSuccess
                ? TypedResults.NoContent()
                : TypedResults.BadRequest(result.Data);
        }
    }
}
