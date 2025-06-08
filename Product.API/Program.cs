using Common.Logging;
using Product.API.Extensions;
using Serilog;
using Serilog.Extensions.Hosting; 

var builder = WebApplication.CreateBuilder(args);
Log.Information("Starting Product API...");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfiguration();
    builder.Services.AddInfrastructure();
    var app = builder.Build();
    app.UseInfrastructure();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Product API failed to start");
}
finally
{
    Log.Information("Product API is shutting down...");
    Log.CloseAndFlush();
}