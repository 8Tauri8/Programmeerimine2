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
    public class QuanityControllerTests
    {
        private readonly Mock<IQuantityService> _QuantityServiceMock;
        private readonly QuantitiesController _controller;

        public QuanityControllerTests()
        {
            _QuantityServiceMock = new Mock<IQuantityService>();
            _controller = new QuantitiesController(_QuantityServiceMock.Object);
        }
        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Quantity>
            {
                new Quantity { id = 1, Nutrients = 23, Amount = 2},
                new Quantity { id = 2, Nutrients = 33, Amount = 6},
            };
            var pagedResult = new PagedResult<Quantity> { Results = data };
            _QuantityServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}