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
    public class PatientsControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public PatientsControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_patient()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("id", "0");
            formValues.Add("Name", "Test");
            formValues.Add("HealthData", "Test");
            formValues.Add("Nutrition", "Test");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Patients/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            var patient = _context.Patient.FirstOrDefault();
            Assert.NotNull(patient);
            Assert.NotEqual(0, patient.id);
            Assert.Equal("Test", patient.Name);
            Assert.Equal("Test", patient.HealthData);
            Assert.Equal("Test", patient.Nutrition);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_patient()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Name", "");
            formValues.Add("HealthData", "");
            formValues.Add("Nutrition", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Patients/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Patient.Any());
        }
    }
}
