using System;
using System.Net.Http;
using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc.Testing;

namespace KooliProjekt.IntegrationTests.Helpers
{
    public abstract class TestBase : IDisposable
    {
        protected WebApplicationFactory<FakeStartup> Factory { get; }

        public TestBase()
        {
            Factory = new TestApplicationFactory<FakeStartup>();
        }

        public HttpClient CreateClient()
        {
            return Factory.CreateClient();
        }

        public ApplicationDbContext GetDbContext()
        {
            return (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        public void Dispose()
        {
            var dbContext = GetDbContext();
            if (dbContext != null)
            {
                dbContext.Database.EnsureDeleted();
            }
            Factory.Dispose();
        }
    }
}
