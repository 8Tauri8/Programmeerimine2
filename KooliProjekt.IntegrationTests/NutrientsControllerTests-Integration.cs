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
    public class NutrientsControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public NutrientsControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_nutrient()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("id", "0");
            formValues.Add("Name", "Test");
            formValues.Add("Sugar", "10");
            formValues.Add("Fat", "5");
            formValues.Add("Carbohydrates", "20");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Nutrients/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            var nutrient = _context.Nutrients.FirstOrDefault();
            Assert.NotNull(nutrient);
            Assert.NotEqual(0, nutrient.id);
            Assert.Equal("Test", nutrient.Name);
            Assert.Equal(10, nutrient.Sugar);
            Assert.Equal(5, nutrient.Fat);
            Assert.Equal(20, nutrient.Carbohydrates);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_nutrient()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Name", "");
            formValues.Add("Sugar", "");
            formValues.Add("Fat", "");
            formValues.Add("Carbohydrates", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Nutrients/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Nutrients.Any());
        }
    }
}
