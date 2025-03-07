using System;
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
    public class NutritionsControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public NutritionsControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_nutrition()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("id", "0");
            formValues.Add("Eating_time", DateTime.Now.ToString());
            formValues.Add("Nutrients", "10");
            formValues.Add("Quantity", "5");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Nutritions/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            var nutrition = _context.Nutrition.FirstOrDefault();
            Assert.NotNull(nutrition);
            Assert.NotEqual(0, nutrition.id);
            Assert.Equal(10, nutrition.Nutrients);
            Assert.Equal(5, nutrition.Quantity);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_nutrition()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Eating_time", "");
            formValues.Add("Nutrients", "");
            formValues.Add("Quantity", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Nutritions/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Nutrition.Any());
        }
    }
}
