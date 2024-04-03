using BlazorApp21.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared_Library.ProductRepository;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<IproductRepository, ProductService>();
builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});
await builder.Build().RunAsync();