using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Responses;

namespace OrderSales.Core.Services;

public interface ICustomerService
{
    Task<Response<Customer?>> CreateAsync(CustomerCreateRequest request);
    Task<Response<Customer?>> UpdateAsync(CustomerUpdateRequest request);
    Task<Response<Customer?>> GetByIdAsync(CustomerGetByIdRequest request);
    Task<Response<Customer?>> DeleteAsync(CustomerDeleteRequest request);
    Task<Response<List<Customer>?>> GetAllAsync(CustomerGetAllRequest request);
}