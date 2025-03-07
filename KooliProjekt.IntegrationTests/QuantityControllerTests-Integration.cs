using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class QuantitiesControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public QuantitiesControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_quantity()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("id", "0");
            formValues.Add("Nutrients", "10");
            formValues.Add("Amount", "5");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Quantities/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var quantity = _context.Quantity.FirstOrDefault();
                Assert.NotNull(quantity);
                Assert.NotEqual(0, quantity.id);
                Assert.Equal(10, quantity.Nutrients);
                Assert.Equal(5, quantity.Amount);
            }
            else
            {
                Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_quantity()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Nutrients", "");
            formValues.Add("Amount", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Quantities/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Quantity.Any());
        }
    }
}
