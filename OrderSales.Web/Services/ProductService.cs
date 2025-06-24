using Microsoft.Extensions.Configuration;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;
using System.Net.Http;
using System.Net.Http.Json;

namespace OrderSales.Web.Services
{
    public class ProductService(IHttpClientFactory client) : IProductService
    {
        private readonly HttpClient _client = client.CreateClient(Configuration.HttpClientName);
       
        public async Task<Response<Product?>> CreateAsync(ProductCreateRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/products", request);
        
            return await result.Content.ReadFromJsonAsync<Response<Product?>>()
                   ?? new Response<Product?>(null, 400, "Falha ao criar a produto");
        }

        public async Task<Response<Product?>> DeleteAsync(ProductDeleteRequest request)
        {
            var result = await _client.DeleteAsync($"v1/products/{request.Id}");

            //implemntar o retorno correto
            throw new NotImplementedException();
        }

        public async Task<Response<List<Product>?>> GetAllAsync(ProductGetAllRequest request)
          => await _client.GetFromJsonAsync<Response<List<Product>?>>("v1/products") ??
                new Response<List<Product>?>(null, 400, "Não foi possivel listar produtos");
        
        public async Task<Response<Product?>> GetByIdAsync(ProductGetByIdRequest request)
        {
            var result = await _client.GetFromJsonAsync<Response<Product?>>($"v1/products/{request.Id}");
            //implemntar o retorno correto
            throw new NotImplementedException();
        }

        public async Task<Response<Product?>> UpdateAsync(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
