using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrderSales.Core.Services;
using OrderSales.Web;
using OrderSales.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
 

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddHttpClient(Configuration.HttpClientName, opt => 
    { opt.BaseAddress = new Uri(Configuration.BackendUrl); });


builder.Services.AddTransient<IProductService, ProductService>();

await builder.Build().RunAsync();