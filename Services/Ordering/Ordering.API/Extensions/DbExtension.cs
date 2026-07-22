using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Ordering.API.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(
            this IHost host,
            Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            var logger =
                services.GetRequiredService<ILogger<TContext>>();

            var context =
                services.GetRequiredService<TContext>();

            try
            {
                logger.LogInformation(
                    "Started Db Migration: {ContextName}",
                    typeof(TContext).Name);

                var retry = Policy
                    .Handle<SqlException>()
                    .WaitAndRetry(
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt =>
                            TimeSpan.FromSeconds(
                                Math.Pow(2, retryAttempt)),
                        onRetry: (exception, span, count) =>
                        {
                            logger.LogWarning(
                                exception,
                                "Retrying database connection. Attempt: {Count}",
                                count);
                        });

                retry.Execute(() =>
                {
                    context.Database.Migrate();

                    seeder(context, services);
                });

                logger.LogInformation(
                    "Migration Completed: {ContextName}",
                    typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An error occurred while migrating database: {ContextName}",
                    typeof(TContext).Name);
            }

            return host;
        }
        private static void CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext? context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
