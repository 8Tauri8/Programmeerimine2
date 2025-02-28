using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;
            var data = new List<Nutrients>
            {
                new Nutrients { id = 1, Name = "Tõnis", Sugar = 34, Fat = 34, Carbohydrates = 54 },
                new Nutrients { id = 2, Name = "Siim", Sugar = 12, Fat = 24, Carbohydrates = 56 },
            };
            var pagedResult = new PagedResult<Nutrients>
            {
                Results = data,
                CurrentPage = page,
                PageCount = 1,
                PageSize = pageSize,
                RowCount = 2
            };

            // Mock service method
            _NutrientsServiceMock.Setup(x => x.List(page, pageSize, It.IsAny<NutrientsSearch>()))
                                 .ReturnsAsync(pagedResult);

            var indexModel = new NutrientsIndexModel
            {
                Search = new NutrientsSearch() // Ensure Search is initialized
            };

            // Act
            var result = await _controller.Index(page, indexModel) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");

            var model = result.Model as NutrientsIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }

        [Fact]
        public async Task Index_should_handle_null_model()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;
            var data = new List<Nutrients>
            {
                new Nutrients { id = 1, Name = "Tõnis", Sugar = 34, Fat = 34, Carbohydrates = 54 },
                new Nutrients { id = 2, Name = "Siim", Sugar = 12, Fat = 24, Carbohydrates = 56 },
            };
            var pagedResult = new PagedResult<Nutrients>
            {
                Results = data,
                CurrentPage = page,
                PageCount = 1,
                PageSize = pageSize,
                RowCount = 2
            };

            // Mock service method
            _NutrientsServiceMock.Setup(x => x.List(page, pageSize, It.IsAny<NutrientsSearch>()))
                                 .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page, null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");

            var model = result.Model as NutrientsIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }

        [Fact]
        public async Task Index_should_handle_null_search()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;
            var data = new List<Nutrients>
            {
                new Nutrients { id = 1, Name = "Tõnis", Sugar = 34, Fat = 34, Carbohydrates = 54 },
                new Nutrients { id = 2, Name = "Siim", Sugar = 12, Fat = 24, Carbohydrates = 56 },
            };
            var pagedResult = new PagedResult<Nutrients>
            {
                Results = data,
                CurrentPage = page,
                PageCount = 1,
                PageSize = pageSize,
                RowCount = 2
            };

            // Mock service method
            _NutrientsServiceMock.Setup(x => x.List(page, pageSize, It.IsAny<NutrientsSearch>()))
                                 .ReturnsAsync(pagedResult);

            var indexModel = new NutrientsIndexModel();

            // Act
            var result = await _controller.Index(page, indexModel) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");

            var model = result.Model as NutrientsIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            int? id = null;

            var result = await _controller.Details(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_is_missing()
        {
            int id = 1;
            var list = (Nutrients)null;
            _NutrientsServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            var result = await _controller.Details(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_list_was_found()
        {
            int id = 1;
            var list = new Nutrients { id = id };
            _NutrientsServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            var result = await _controller.Details(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public void Create_should_return_view()
        {
            var result = _controller.Create() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create");
        }

        [Fact]
        public async Task Create_should_save_and_redirect_when_model_is_valid()
        {
            var nutrients = new Nutrients { id = 1, Name = "Test", Sugar = 10, Fat = 5, Carbohydrates = 20 };
            _NutrientsServiceMock.Setup(x => x.Save(It.IsAny<Nutrients>())).Returns(Task.CompletedTask).Verifiable();

            var result = await _controller.Create(nutrients) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _NutrientsServiceMock.Verify(x => x.Save(nutrients), Times.Once);
        }

        [Fact]
        public async Task Create_should_return_view_when_model_is_invalid()
        {
            var nutrients = new Nutrients { id = 1 };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Create(nutrients) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(nutrients, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            int? id = null;

            var result = await _controller.Edit(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_list_is_missing()
        {
            int id = 1;
            var list = (Nutrients)null;
            _NutrientsServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            var result = await _controller.Edit(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_list_was_found()
        {
            int id = 1;
            var list = new Nutrients { id = id };
            _NutrientsServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            var result = await _controller.Edit(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async Task Edit_Post_should_return_notfound_when_id_does_not_match_model_id()
        {
            int urlId = 1;
            var nutrients = new Nutrients { id = 2, Name = "Test", Sugar = 10, Fat = 5, Carbohydrates = 20 };
            _NutrientsServiceMock.Setup(x => x.Save(It.IsAny<Nutrients>())).Verifiable();

            var result = await _controller.Edit(urlId, nutrients) as NotFoundResult;

            Assert.NotNull(result);
            _NutrientsServiceMock.Verify(x => x.Save(It.IsAny<Nutrients>()), Times.Never);
        }

        [Fact]
        public async Task Edit_Post_should_save_and_redirect_when_model_is_valid()
        {
            int urlId = 1;
            var nutrients = new Nutrients { id = urlId, Name = "Test", Sugar = 10, Fat = 5, Carbohydrates = 20 };
            _NutrientsServiceMock.Setup(x => x.Save(It.IsAny<Nutrients>())).Returns(Task.CompletedTask).Verifiable();

            var result = await _controller.Edit(urlId, nutrients) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _NutrientsServiceMock.Verify(x => x.Save(nutrients), Times.Once);
        }

        [Fact]
        public async Task Edit_Post_should_return_view_when_model_is_invalid()
        {
            int urlId = 1;
            var nutrients = new Nutrients { id = urlId };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Edit(urlId, nutrients) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(nutrients, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            int? id = null;

            var result = await _controller.Delete(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_list_is_missing()
        {
            int id = 1;
            var list = (Nutrients)null;
            _NutrientsServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            var result = await _controller.Delete(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_list_was_found()
        {
            int id = 1;
            var list = new Nutrients { id = id };
            _NutrientsServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

            var result = await _controller.Delete(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_and_redirect_when_valid_id()
        {
            int id = 1;
            _NutrientsServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask).Verifiable();

            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _NutrientsServiceMock.Verify(x => x.Delete(id), Times.Once);
        }
    }
}
