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
    public class NutritionsControllerTests
    {
        private readonly Mock<INutritionService> _NutritionsServiceMock;
        private readonly NutritionsController _controller;

        public NutritionsControllerTests()
        {
            _NutritionsServiceMock = new Mock<INutritionService>();
            _controller = new NutritionsController((ApplicationDbContext)_NutritionsServiceMock.Object);
        }
        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Nutrition>
            {
                new Nutrition { id = 1, Eating_time = new DateTime(2019,05,09), Nutrients = 34, Quantity = 12},
                new Nutrition { id = 2, Eating_time = new DateTime(2025,12,10), Nutrients = 23, Quantity = 32},

            };
            var pagedResult = new PagedResult<Nutrition> { Results = data };
            _NutritionsServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}