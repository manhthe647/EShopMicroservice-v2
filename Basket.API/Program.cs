using Basket.API.Extensions;
using Common.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // LoggerConfiguration
    .CreateBootstrapLogger(); // ReloadableLogger

var builder = WebApplication.CreateBuilder(args);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
    // Add services to the container.
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();

    builder.Services.Configure<RouteOptions>(options =>
        options.LowercaseUrls = true);

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    Log.Error(ex, "Basket API failed to start with exception type: {ExceptionType}", type);
}
finally
{
    Log.Information("Basket API is shutting down...");
    Log.CloseAndFlush();
}