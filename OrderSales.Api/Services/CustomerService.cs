using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OrderSales.Api.Data;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Responses;
using OrderSales.Core.Services;

namespace OrderSales.Api.Services;

public class CustomerService(AppDbContext context) : ICustomerService
{
    private readonly AppDbContext _context = context;

    public async Task<Response<Customer?>> CreateAsync(CustomerCreateRequest request)
    {
        try
        {
            var customer = new Customer
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                Phone = request.Phone
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return new Response<Customer?>(customer, 200, "Cliente Cadastrado !");
        }
        catch
        {
            return new Response<Customer?>(null, 400, "Ocorreu um erro ao Salvar Cliente");
        }
    }

    public async Task<Response<Customer?>> UpdateAsync(CustomerUpdateRequest request)
    {
        try
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

            customer.Name = request.Name;
            customer.Address = request.Address;
            customer.Email = request.Email;
            customer.Phone = request.Phone;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return new Response<Customer?>(customer, 200, "Cliente atualizado !");
        }
        catch 
        {
            return new Response<Customer?>(null, 400, "Erro ao atualizar Cliente!");
        }

    }

    public async Task<Response<Customer?>> GetByIdAsync(CustomerGetByIdRequest request)
    {
        try
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

            return new Response<Customer?>(customer, 200, null);

        }
        catch 
        {
            return new Response<Customer?>(null, 400, "Erro ao recuperar cliente");
        }

    }

    public async Task<Response<Customer?>> DeleteAsync(CustomerDeleteRequest request)
    {
        try
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return new Response<Customer?>(customer, 200, "Cliente Excluído com sucesso !");
        }
        catch 
        {
            return new Response<Customer?>(null, 400, "Erro ao excluir Cliente!");
        }
    }

    public async Task<Response<List<Customer>?>> GetAllAsync(CustomerGetAllRequest request)
    {
        try
        {
            var customers = await _context
                .Customers.AsNoTracking()
                .ToListAsync();

            return new Response<List<Customer>?>(customers, 200, null);
        }
        catch (Exception e)
        {
            return new Response<List<Customer>?>(null, 400, "Erro ao recuperar clientes!");
        }
    }
}