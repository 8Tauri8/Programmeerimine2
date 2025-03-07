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
    public class HealthDatasControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public HealthDatasControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_health_data()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("id", "0");
            formValues.Add("Weight", "70");
            formValues.Add("Blood_pressure", "120");
            formValues.Add("Blood_sugar", "5");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/HealthDatas/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            var healthData = _context.HealthData.FirstOrDefault();
            Assert.NotNull(healthData);
            Assert.NotEqual(0, healthData.id);
            Assert.Equal(70, healthData.Weight);
            Assert.Equal(120, healthData.Blood_pressure);
            Assert.Equal(5, healthData.Blood_sugar);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_health_data()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Weight", "");
            formValues.Add("Blood_pressure", "");
            formValues.Add("Blood_sugar", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/HealthDatas/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.HealthData.Any());
        }
    }
}
