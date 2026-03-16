using Common.Logging;
using Customers.API.Controllers;
using Customers.API.Persistence;
using Customers.API.Services;
using Customers.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Starting Customers.API");

try
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = builder.Configuration.GetConnectionString("CustomerConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'CustomerConnection' is not configured.");
    }

    builder.Services.AddDbContext<CustomerContext>(options =>
        options.UseNpgsql(connectionString));

    builder.Services.AddScoped<Customers.API.Repositories.Interfaces.ICustomerRepository,
                               Customers.API.Repositories.CustomerRepositoryAsync>()
                    .AddScoped<ICustomerService, CustomerService>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();
    app.MapControllers();
    app.MapGet("/", () => "Welcome to Customers API!");
    app.MapCustomersAPI();

    app.SeedCustomerData(); // ← bỏ await

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}