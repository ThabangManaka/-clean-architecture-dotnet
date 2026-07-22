using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextFactory
    : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "../Ordering.API"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            var connectionString =
                configuration.GetConnectionString("OrderingConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();

            optionsBuilder.UseSqlServer(
                connectionString,
                sql =>
                {
                    // 🔑 THIS IS WHAT EF CLI USES
                    sql.MigrationsAssembly("Ordering.Infrastructure");
                });

            return new OrderContext(optionsBuilder.Options);
        }
    }
}
