using Microsoft.AspNetCore.Mvc;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace OrderSales.Api.EndPoints.Customers;

public class CreateCustomerEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", ServiceAsync)
            .WithName("Customers : Create")
            .WithSummary("Cadastra um novo cliente")
            .WithOrder(1)
            .Produces<Response<Customer?>>();
    }

    private static async Task<IResult> ServiceAsync(
        ICustomerService CustomerService,
        [FromBody] CustomerCreateRequest request
    )
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request);

        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            var errors = validationResults.Select(vr => vr.ErrorMessage);
            return Results.BadRequest(new { Errors = errors });
        }


        var result = await CustomerService.CreateAsync(request);

        return result.IsSuccess
            ? TypedResults.Created($"{result.Data?.Id}",result.Data)
            : TypedResults.BadRequest(result.Data);
    }
}