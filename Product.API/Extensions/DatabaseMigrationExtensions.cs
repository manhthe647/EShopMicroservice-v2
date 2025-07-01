using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Product.API.Extensions
{
    public static class DatabaseMigrationExtensions
    {
        public static async Task<WebApplication> MigrateDatabaseWithSeedAsync<TContext>(this WebApplication app,
         Func<TContext, Task> seedAction) where TContext : DbContext
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TContext>();

                Log.Information("Starting database migration for {ContextType}", typeof(TContext).Name);
                context.Database.Migrate();
                Log.Information("Database migration completed successfully for {ContextType}", typeof(TContext).Name);

                // Run seeding
                Log.Information("Starting database seeding for {ContextType}", typeof(TContext).Name);
                await seedAction(context);
                Log.Information("Database seeding completed successfully for {ContextType}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while migrating/seeding the database for {ContextType}", typeof(TContext).Name);
                throw;
            }
            return app;
        }

        // Alternative method that allows you to specify whether to throw on migration failure
        public static WebApplication MigrateDatabase<TContext>(this WebApplication app, bool throwOnError = true)
            where TContext : DbContext
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<TContext>();
                Log.Information("Starting database migration for {ContextType}", typeof(TContext).Name);

                context.Database.Migrate();

                Log.Information("Database migration completed successfully for {ContextType}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while migrating the database for {ContextType}", typeof(TContext).Name);

                if (throwOnError)
                    throw;
            }

            return app;
        }
    }
}