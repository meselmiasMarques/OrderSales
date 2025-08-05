using Microsoft.EntityFrameworkCore;
using OrderSales.Api.Data;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.Services
{
    public class ProductService(AppDbContext context) : IProductService
    {
        private readonly AppDbContext _context = context;

        public async Task<Response<Product?>> CreateAsync(ProductCreateRequest request)
        {
            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    Stock = request.Stock
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return new Response<Product?>(product, 201, "Produto Criado com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Product?>(null, 400, $"Erro ao Cadastrar Produto - {ex.Message}");
            }
        }

        public async Task<Response<Product?>> UpdateAsync(ProductUpdateRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (product == null)
            {
                return new Response<Product?>(null, 404, "Produto não encontrado");
            }
            else
            {
                try
                {
                    product.Name = request.Name;
                    product.Price = request.Price;
                    product.Stock = request.Stock;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                    return new Response<Product?>(product, 200, "Produto atualizado com sucesso");
                }
                catch (Exception ex)
                {
                    return new Response<Product?>(null, 400, $"Erro ao atualizar Produto - {ex.Message}");
                }
            }
        }

        public async Task<Response<Product?>> GetByIdAsync(ProductGetByIdRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (product == null)
            {
                return new Response<Product?>(null, 404, "Produto não encontrado ");
            }

            return new Response<Product?>(product, 200);
        }

        public async Task<Response<Product?>> DeleteAsync(ProductDeleteRequest request)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (product == null)
                {
                    return new Response<Product?>(null, 404, "Produto não encontrado");
                }
                product.active = 0; // Set active to 0 to mark as inactive
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return new Response<Product?>(null, 200, "Produto excluído com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Product?>(null, 400, $"Erro ao excluir produto - {ex.Message}");

            }
        }

        public async Task<Response<List<Product>?>> GetAllAsync(ProductGetAllRequest request)
        {
            try
            {
                var products = await _context
                    .Products.AsNoTracking()
                    .ToListAsync();
                return new Response<List<Product>?>(products, 200, "");
            }
            catch (Exception ex)
            {
                return new Response<List<Product>?>(null, 400, $"Erro ao listar Produtos - {ex.Message}");
            }
        }
    }
}
