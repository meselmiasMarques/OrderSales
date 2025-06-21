using OrderSales.Api.EndPoints.Customers;
using OrderSales.Api.EndPoints.Orders;
using OrderSales.Api.EndPoints.Products;
using OrderSales.Core.Requests.Customers;

namespace OrderSales.Api.EndPoints;

public static class EndPoints
{
    public static void MapEndPoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("v1/customers")
            .WithTags("Customers")
            .MapEndpoint<GetAllCustomerEndPoint>()
            .MapEndpoint<CreateCustomerEndPoint>()
            .MapEndpoint<UpdateCustomerEndPoint>()
            .MapEndpoint<DeleteCustomerEndPoint>()
            .MapEndpoint<GetByIdCustomerEndPoint>();


        endpoints.MapGroup("v1/products")
            .WithTags("Products")
            .MapEndpoint<GetAllProductEndPoint>()
            .MapEndpoint<CreateProductEndPoint>()
            .MapEndpoint<UpdateProductEndPoint>()
            .MapEndpoint<DeleteProductEndPoint>()
            .MapEndpoint<GetByIdProductEndPoint>();


        endpoints.MapGroup("v1/orders")
            .WithTags("Orders")
            .MapEndpoint<GetAllOrderEndPoint>()
            .MapEndpoint<GetByIdOrderEndPoint>()
            .MapEndpoint<GetOrderByCustomerEndPoint>()
            .MapEndpoint<CreateOrderEndPoint>();
    }
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}