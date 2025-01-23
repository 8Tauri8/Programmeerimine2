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
    public class NutrientsControllerTests
    {
        private readonly Mock<INutrientsService> _NutrientsServiceMock;
        private readonly NutrientsController _controller;

        public NutrientsControllerTests()
        {
            _NutrientsServiceMock = new Mock<INutrientsService>();
            _controller = new NutrientsController(_NutrientsServiceMock.Object);
        }
        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Nutrients>
            {
                new Nutrients { id = 1, Name = "Tõnis", Sugar = 34, Fat = 34, Carbohydrates = 54},
                new Nutrients { id = 2, Name = "Siim", Sugar = 12, Fat = 24, Carbohydrates = 56},

            };
            var pagedResult = new PagedResult<Nutrients> { Results = data };
            _NutrientsServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}