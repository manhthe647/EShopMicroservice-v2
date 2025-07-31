using Common.Logging;
using Contracts.Common.Interfaces;
using Customers.API.Persistence;
using Customers.API.Services;
using Customers.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);
Log.Information("Starting Customers.API");

try
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }
    builder.Services.AddDbContext<Customers.API.Persistence.CustomerContext>(options =>
        options.UseNpgsql(connectionString));

    builder.Services.AddScoped<Customers.API.Repositories.Interfaces.ICustomerRepository, Customers.API.Repositories.CustomerRepositoryAsync>()
        .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
        .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
        .AddScoped(typeof(ICustomerService), typeof(CustomerService));

    var app = builder.Build();
    app.MapGet("/", () => "Welcome to Customers API!");
    app.MapPost("/", () => "Welcome to Customers API!");
    app.MapPut("/", () => "Welcome to Customers API!");
    app.MapDelete("/", () => "Welcome to Customers API!");


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.SeedCustomerData().Run();

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
