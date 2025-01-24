using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class QuantitiesControllerTests
    {
        private readonly Mock<IQuantityService> _QuantityServiceMock;
        private readonly QuantitiesController _controller;

        public QuantitiesControllerTests()
        {
            _QuantityServiceMock = new Mock<IQuantityService>();
            _controller = new QuantitiesController(_QuantityServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            var page = 1;
            var data = new List<Quantity>
            {
                new Quantity { id = 1, Nutrients = 23, Amount = 2 },
                new Quantity { id = 2, Nutrients = 33, Amount = 6 }
            };
            var pagedResult = new PagedResult<Quantity>
            {
                Results = data,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 5,
                RowCount = 2
            };
            _QuantityServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
            Assert.Equal(pagedResult, result.Model);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var quantity = (Quantity)null;
            _QuantityServiceMock.Setup(x => x.Get(id)).ReturnsAsync(quantity);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var quantity = new Quantity { id = id };
            _QuantityServiceMock.Setup(x => x.Get(id)).ReturnsAsync(quantity);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(quantity, result.Model);
        }

        [Fact]
        public void Create_should_return_view()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create");
        }

        [Fact]
        public async Task Create_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            var quantity = new Quantity { id = 1, Nutrients = 10, Amount = 5 };
            _QuantityServiceMock.Setup(x => x.Save(It.IsAny<Quantity>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.Create(quantity) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _QuantityServiceMock.Verify(x => x.Save(quantity), Times.Once);
        }

        [Fact]
        public async Task Create_should_return_view_when_model_is_invalid()
        {
            // Arrange
            var quantity = new Quantity { id = 1 };
            _controller.ModelState.AddModelError("Nutrients", "Required");

            // Act
            var result = await _controller.Create(quantity) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(quantity, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var quantity = (Quantity)null;
            _QuantityServiceMock.Setup(x => x.Get(id)).ReturnsAsync(quantity);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var quantity = new Quantity { id = id };
            _QuantityServiceMock.Setup(x => x.Get(id)).ReturnsAsync(quantity);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(quantity, result.Model);
        }

        [Fact]
        public async Task Edit_Post_should_return_notfound_when_id_does_not_match_model_id()
        {
            // Arrange
            int urlId = 1;
            var quantity = new Quantity { id = 2, Nutrients = 15, Amount = 3 };

            // Act
            var result = await _controller.Edit(urlId, quantity) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_Post_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            int urlId = 1;
            var quantity = new Quantity { id = urlId, Nutrients = 15, Amount = 3 };
            _QuantityServiceMock.Setup(x => x.Save(It.IsAny<Quantity>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.Edit(urlId, quantity) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _QuantityServiceMock.Verify(x => x.Save(quantity), Times.Once);
        }

        [Fact]
        public async Task Edit_Post_should_return_view_when_model_is_invalid()
        {
            // Arrange
            int urlId = 1;
            var quantity = new Quantity { id = urlId };
            _controller.ModelState.AddModelError("Nutrients", "Required");

            // Act
            var result = await _controller.Edit(urlId, quantity) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(quantity, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var quantity = (Quantity)null;
            _QuantityServiceMock.Setup(x => x.Get(id)).ReturnsAsync(quantity);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var quantity = new Quantity { id = id };
            _QuantityServiceMock.Setup(x => x.Get(id)).ReturnsAsync(quantity);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(quantity, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_and_redirect_when_valid_id()
        {
            // Arrange
            int id = 1;
            _QuantityServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _QuantityServiceMock.Verify(x => x.Delete(id), Times.Once);
        }
    }
}
