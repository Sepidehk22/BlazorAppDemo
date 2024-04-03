using Azure.Identity;
using BlazorApp21.Client.Pages;
using BlazorApp21.Components;
using BlazorApp21.Data;
using BlazorApp21.Implementations;
using Microsoft.EntityFrameworkCore;
using Shared_Library.ProductRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var configuration = builder.Configuration;
if (builder.Environment.IsProduction())
{
    configuration.AddAzureKeyVault(
        new Uri("https://blazorkeyvaultdemo.vault.azure.net/"),
        new DefaultAzureCredential());
}

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDConnection") ?? throw new InvalidOperationException("Connection is not found"));
});

builder.Services.AddScoped<IproductRepository, ProductRepository>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress").Value!)
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp21.Client._Imports).Assembly);

app.Run();
