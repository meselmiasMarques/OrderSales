using OrderSales.Core.Models;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Responses;

namespace OrderSales.Core.Services;

public interface IProductService
{
    Task<Response<Product?>> CreateAsync(ProductCreateRequest request);
    Task<Response<Product?>> UpdateAsync(ProductUpdateRequest request);
    Task<Response<Product?>> GetByIdAsync(ProductGetByIdRequest request);
    Task<Response<Product?>> DeleteAsync(ProductDeleteRequest request);
    Task<Response<List<Product>?>> GetAllAsync(ProductGetAllRequest request);

}