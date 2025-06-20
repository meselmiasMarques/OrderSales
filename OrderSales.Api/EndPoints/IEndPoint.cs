namespace OrderSales.Api.EndPoints;

public interface IEndPoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}