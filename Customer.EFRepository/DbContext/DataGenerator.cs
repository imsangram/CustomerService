using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Customer.EFRepository
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CustomerDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<CustomerDBContext>>()))
            {
                // Look for any Customers already in database.
                if (context.Customers.Any())
                {
                    return;   // Database has been seeded
                }

                context.Customers.AddRange(
                    new Entity.Customer
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        DateOfBirth = new DateTime(1988,12,12)
                    },
                    new Entity.Customer
                    {
                        Id = 2,
                        FirstName = "John",
                        LastName = "Lee",
                        DateOfBirth = new DateTime(1989, 12, 02)
                    },
                    new Entity.Customer
                    {
                        Id = 3,
                        FirstName = "Jake",
                        LastName = "Peralta",
                        DateOfBirth = new DateTime(1985, 12, 09)
                    },
                    new Entity.Customer
                    {
                        Id = 4,
                        FirstName = "Jane",
                        LastName = "Doe",
                        DateOfBirth = new DateTime(1965, 12, 06)
                    },
                    new Entity.Customer
                    {
                        Id = 5,
                        FirstName = "Brad",
                        LastName = "Pitt",
                        DateOfBirth = new DateTime(1967, 01, 01)
                    },
                    new Entity.Customer
                    {
                        Id = 6,
                        FirstName = "Sam",
                        LastName = "Diaz",
                        DateOfBirth = new DateTime(2000, 12, 05)
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}
