using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging
{
    public static class Serilogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, config) =>
        {
            var appName = context.HostingEnvironment.ApplicationName?.ToLower().Replace('.', '-');
            var env = context.HostingEnvironment.EnvironmentName ?? "Development";

            config
                // Ghi log ra Console với mẫu
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", env)
                .Enrich.WithProperty("Application", appName)
                // Đọc cấu hình Serilog từ appsettings.json
                .ReadFrom.Configuration(context.Configuration);
        };
    }
}