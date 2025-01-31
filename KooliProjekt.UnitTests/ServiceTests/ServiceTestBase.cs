using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ServiceTestBase : IDisposable
    {
        protected readonly ApplicationDbContext DbContext;

        public ServiceTestBase()
        {
            // Create an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "HealthDataTestDb")
                .Options;

            DbContext = new ApplicationDbContext(options);
            DbContext.Database.EnsureCreated(); // Ensure the in-memory database is created
        }

        public void Dispose()
        {
            DbContext.Dispose(); // Dispose the DbContext after each test
        }
    }
}
