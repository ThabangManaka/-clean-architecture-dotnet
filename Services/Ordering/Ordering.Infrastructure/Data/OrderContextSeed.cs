using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new()
                {
                    UserName = "Tman",
                    FirstName = "Tman",
                    LastName = "Dan",
                    EmailAddress = "thabang@man.net",
                    AddressLine = "Ganda",
                    State = "GP",
                    Country = "South Africa",
                    ZipCode = "012",

                    CardName = "Visa",
                    CardNumber = "4111111111111111",
                    CreatedBy = "Tman",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "Tman",
                    LastModifiedDate = DateTime.UtcNow
                }
            };
        }
    }
}
