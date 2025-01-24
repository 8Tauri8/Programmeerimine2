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
    public class PatientsControllerTests
    {
        private readonly Mock<IPatientService> _PatientsServiceMock;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _PatientsServiceMock = new Mock<IPatientService>();
            _controller = new PatientsController(_PatientsServiceMock.Object);
        }
        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Patient>
            {
                new Patient { id = 1, Name = "Kutsu", HealthData = "Good", Nutrition = "Stable"},
                new Patient { id = 2, Name = "Asvu", HealthData = "Bad", Nutrition = "Bad"},
            };
            var pagedResult = new PagedResult<Patient> { Results = data };
            _PatientsServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}