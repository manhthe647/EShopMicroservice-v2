using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistence;
using Serilog;
using Serilog.Extensions.Hosting; 

var builder = WebApplication.CreateBuilder(args);
Log.Information("Starting Product API...");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfiguration();
    builder.Services.AddInfrastructure(builder.Configuration);
    var app = builder.Build();
    app.UseInfrastructure();
    (await app.MigrateDatabaseWithSeedAsync<ProductContext>(ProductContextSeed.SeedAsync)).Run();
    app.Run();
}
catch (Exception ex)
{
    string type=ex.GetType().Name;
    Log.Error(ex, "Product API failed to start with exception type: {ExceptionType}", type);
}
finally
{
    Log.Information("Product API is shutting down...");
    Log.CloseAndFlush();
}