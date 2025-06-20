using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OrderSales.Api.Data;
using OrderSales.Api.EndPoints;
using OrderSales.Api.Services;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndPoints();

app.Run();
