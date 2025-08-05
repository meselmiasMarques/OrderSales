using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;
using System.Net.Http.Json;

namespace OrderSales.Web.Services
{
    public class CustomerService(IHttpClientFactory client) : ICustomerService
    {
        private readonly HttpClient _client = client.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Customer?>> CreateAsync(CustomerCreateRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/customers", request);

            return await result.Content.ReadFromJsonAsync<Response<Customer?>>()
                   ?? new Response<Customer?>(null, 400, "Falha ao criar o cliente");
        }

        public async Task<Response<Customer?>> DeleteAsync(CustomerDeleteRequest request)
        {
            var result = await _client.DeleteAsync($"v1/customers/{request.Id}");

            return await result.Content.ReadFromJsonAsync<Response<Customer?>>()
                   ?? new Response<Customer?>(null, 400, "Falha ao deletar o cliente");
        }

        public async Task<Response<List<Customer>?>> GetAllAsync(CustomerGetAllRequest request)
            => await _client.GetFromJsonAsync<Response<List<Customer>?>>("v1/customers") ??
              new Response<List<Customer>?> (null, 400, "Não foi possivel listar clientes");

        public async Task<Response<Customer?>> GetByIdAsync(CustomerGetByIdRequest request)
        {
            var result = await _client.GetFromJsonAsync<Response<Customer?>>($"v1/customers/{request.Id}");
            return result ?? new Response<Customer?>(null, 400, "Cliente não encontrado");
        }

        public async Task<Response<Customer?>> UpdateAsync(CustomerUpdateRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/customers/{request.Id}", request);

            return await result.Content.ReadFromJsonAsync<Response<Customer?>>()
                   ?? new Response<Customer?>(null, 400, "Falha ao atualizar o cliente");
        }
    }
}
