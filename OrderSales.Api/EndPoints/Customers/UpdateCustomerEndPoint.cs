using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.EndPoints.Customers
{
    public class UpdateCustomerEndPoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", ServiceAsync)
                .WithName("Customers : Update")
                .WithSummary("Atualiza clientes")
                .WithOrder(4)
                .Produces<Response<Customer?>>();
        }

        private static async Task<IResult> ServiceAsync(
            ICustomerService CustomerService,
            Guid id,
            CustomerUpdateRequest request
        )
        {
            var customer = new CustomerUpdateRequest
            {
                Id = id,
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                Phone = request.Phone
            };
            var result = await CustomerService.UpdateAsync(customer);

            return result.IsSuccess
                ? TypedResults.Ok(result.Data)
                : TypedResults.BadRequest(result.Data);
        }
    }
}
