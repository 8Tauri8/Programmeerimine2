using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HealthDatasControllerTests
    {
        private readonly Mock<IHealthDataService> _healthDataServiceMock;
        private readonly HealthDatasController _controller;

        public HealthDatasControllerTests()
        {
            _healthDataServiceMock = new Mock<IHealthDataService>();
            _controller = new HealthDatasController(_healthDataServiceMock.Object);
        }

        [Fact]
        public async Task Create_should_save_and_redirect_when_model_is_valid()
        {
            var healthData = new HealthData { id = 1, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };

            _healthDataServiceMock.Setup(x => x.Save(It.IsAny<HealthData>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var result = await _controller.Create(healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Verifying it redirects to Index
            _healthDataServiceMock.Verify(x => x.Save(healthData), Times.Once); // Verify Save method was called
        }

        [Fact]
        public async Task Edit_Post_Should_Save_And_Redirect_When_Model_Is_Valid()
        {
            var healthData = new HealthData { id = 1, Weight = 75, Blood_pressure = 130, Blood_sugar = 95 };

            _healthDataServiceMock.Setup(x => x.Save(It.IsAny<HealthData>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var result = await _controller.Edit(healthData.id, healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _healthDataServiceMock.Verify(x => x.Save(healthData), Times.Once);
        }
    }
}
