using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HealthDataControllerTests
    {
        private readonly Mock<IHealthDataService> _HealthDataServiceMock;
        private readonly HealthDatasController _controller;

        public HealthDataControllerTests()
        {
            _HealthDataServiceMock = new Mock<IHealthDataService>();
            _controller = new HealthDatasController(_HealthDataServiceMock.Object);
        }
        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<HealthData>
            {
                new HealthData { id = 1, Weight = 68, Blood_pressure = 10, Blood_sugar = 32},
                new HealthData { id = 2, Weight = 168, Blood_pressure = 20, Blood_sugar = 38},
            };
            var pagedResult = new PagedResult<HealthData> { Results = data };
            _HealthDataServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}