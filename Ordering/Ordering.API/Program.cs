using Common.Logging;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using Serilog;
using Serilog.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
Log.Information("Starting Ordering API...");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);

    builder.Services.AddInfrastructureServices(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    Log.Error(ex, "Ordering API failed to start with exception type: {ExceptionType}", type);
}
finally
{
    Log.Information("Ordering API is shutting down...");
    Log.CloseAndFlush();
}