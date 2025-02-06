using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ServiceTestBase : IDisposable
    {
        protected readonly ApplicationDbContext DbContext;

        public ServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            DbContext = new ApplicationDbContext(options);
            DbContext.Database.EnsureCreated(); // Ensure the database is created

        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted(); // Clean up after each test
            DbContext.Dispose();
        }
    }
}
