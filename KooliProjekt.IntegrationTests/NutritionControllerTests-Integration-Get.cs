using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class NutritionControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public NutritionControllerTests()
        {
            _client = Factory.CreateClient();
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        public async Task Index_should_return_correct_response()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Nutrition/Index");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Nutrition/Details/");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_does_not_exist()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Nutrition/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_ok_when_id_exists()
        {
            // Arrange
            var nutrition = new Nutrition { Eating_time = DateTime.Now, Nutrients = 10, Quantity = 5 };
            _context.Nutrition.Add(nutrition);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/Nutritions/Details/" + nutrition.id); // Muutke URL-i kui vaja

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
