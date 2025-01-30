using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class NutritionsControllerTests
    {
        private readonly Mock<INutritionService> _NutritionServiceMock;
        private readonly NutritionsController _controller;

        public NutritionsControllerTests()
        {
            _NutritionServiceMock = new Mock<INutritionService>();
            _controller = new NutritionsController(_NutritionServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            var page = 1;
            var data = new List<Nutrition>
            {
                new Nutrition { id = 1, Eating_time = new DateTime(2019, 05, 09), Nutrients = 34, Quantity = 12 },
                new Nutrition { id = 2, Eating_time = new DateTime(2025, 12, 10), Nutrients = 23, Quantity = 32 }
            };
            var pagedResult = new PagedResult<Nutrition>
            {
                Results = data,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 5,
                RowCount = 2
            };
            _NutritionServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

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
            var list = (Nutrition)null;
            _NutritionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            var list = new Nutrition { id = id };
            _NutritionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(list, result.Model);
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
            var nutrition = new Nutrition { id = 1, Eating_time = new DateTime(2025, 01, 01), Nutrients = 20, Quantity = 10 };
            _NutritionServiceMock.Setup(x => x.Save(It.IsAny<Nutrition>())).Returns(Task.CompletedTask).Verifiable();

            var result = await _controller.Create(nutrition) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _NutritionServiceMock.Verify(x => x.Save(nutrition), Times.Once);
        }

        [Fact]
        public async Task Create_should_return_view_when_model_is_invalid()
        {
            var nutrition = new Nutrition { id = 1 };
            _controller.ModelState.AddModelError("Eating_time", "Required");

            var result = await _controller.Create(nutrition) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(nutrition, result.Model);
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
            var list = (Nutrition)null;
            _NutritionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            var list = new Nutrition { id = id };
            _NutritionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async Task Edit_Post_should_return_notfound_when_id_does_not_match_model_id()
        {
            // Arrange
            int urlId = 1;
            var nutrition = new Nutrition { id = 2, Eating_time = new DateTime(2025, 01, 01), Nutrients = 20, Quantity = 10 };
            _NutritionServiceMock.Setup(x => x.Save(It.IsAny<Nutrition>())).Verifiable();

            // Act
            var result = await _controller.Edit(urlId, nutrition) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            _NutritionServiceMock.Verify(x => x.Save(It.IsAny<Nutrition>()), Times.Never);
        }

        [Fact]
        public async Task Edit_Post_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            int urlId = 1;
            var nutrition = new Nutrition { id = urlId, Eating_time = new DateTime(2025, 01, 01), Nutrients = 20, Quantity = 10 };
            _NutritionServiceMock.Setup(x => x.Save(It.IsAny<Nutrition>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.Edit(urlId, nutrition) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _NutritionServiceMock.Verify(x => x.Save(nutrition), Times.Once);
        }

        [Fact]
        public async Task Edit_Post_should_return_view_when_model_is_invalid()
        {
            // Arrange
            int urlId = 1;
            var nutrition = new Nutrition { id = urlId };
            _controller.ModelState.AddModelError("Eating_time", "Required");

            // Act
            var result = await _controller.Edit(urlId, nutrition) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nutrition, result.Model);
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
            var list = (Nutrition)null;
            _NutritionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            var list = new Nutrition { id = id };
            _NutritionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_and_redirect_when_valid_id()
        {
            // Arrange
            int id = 1;
            _NutritionServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _NutritionServiceMock.Verify(x => x.Delete(id), Times.Once);
        }
    }
}
