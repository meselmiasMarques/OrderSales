using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Customers
{
    public class GetAllCustomerEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
           app.MapGet("/", ServiceAsync)
               .WithName("Customers : GetAll")
               .WithSummary("Lista todos clientes")
               .WithOrder(2)
               .Produces<Response<Customer?>>();
        }

        private static async Task<IResult> ServiceAsync(
            ICustomerService CustomerService
        )
        {
            var request = new CustomerGetAllRequest();
            var result = await CustomerService.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result.Data)
                : TypedResults.BadRequest(result.Data);
        }
    }
}
