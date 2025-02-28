using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using KooliProjekt.Models;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HealthDatasControllerTests
    {
        private readonly Mock<IHealthDataService> _HealthDataServiceMock;
        private readonly HealthDatasController _controller;

        public HealthDatasControllerTests()
        {
            _HealthDataServiceMock = new Mock<IHealthDataService>();
            _controller = new HealthDatasController(_HealthDataServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;
            var data = new List<HealthData>
            {
                new HealthData { id = 1, Weight = 68, Blood_pressure = 10, Blood_sugar = 32},
                new HealthData { id = 2, Weight = 168, Blood_pressure = 20, Blood_sugar = 38},
            };
            var pagedResult = new PagedResult<HealthData>
            {
                Results = data,
                CurrentPage = page,
                PageCount = 1,
                PageSize = pageSize,
                RowCount = 2
            };

            // Mock the List method, explicitly handling the optional search parameter.
            _HealthDataServiceMock
                .Setup(x => x.List(page, pageSize, It.IsAny<Search.HealthDatasSearch>()))
                .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page, new HealthDatasIndexModel()) as ViewResult; // Pass in a model as parameter

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Index"
            );

            // Correctly compare the model, which should be a HealthDatasIndexModel
            var model = result.Model as HealthDatasIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data); //checks if the pagedResult is equal to the model.Data PagedResult
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
            var list = (HealthData)null;
            _HealthDataServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            var result = await _controller.Details(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_list_was_found()
        {
            int id = 1;
            var list = new HealthData { id = id };
            _HealthDataServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            var result = await _controller.Details(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Details"
            );
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public void Create_should_return_view()
        {
            var result = _controller.Create() as ViewResult;

            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Create"
            );
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
            var list = (HealthData)null;
            _HealthDataServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            var result = await _controller.Edit(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_list_was_found()
        {
            int id = 1;
            var list = new HealthData { id = id };
            _HealthDataServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            var result = await _controller.Edit(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Edit"
            );
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async Task Edit_Post_Should_Return_NotFound_When_Id_Does_Not_Match_Model_Id()
        {
            int urlId = 1;
            var healthData = new HealthData { id = 2, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };
            _HealthDataServiceMock
                .Setup(x => x.Save(It.IsAny<HealthData>()))
                .Verifiable();

            var result = await _controller.Edit(urlId, healthData) as NotFoundResult;

            Assert.NotNull(result);
            _HealthDataServiceMock.Verify(x => x.Save(It.IsAny<HealthData>()), Times.Never);
        }

        [Fact]
        public async Task Edit_Post_Should_Save_And_Redirect_When_Model_Is_Valid()
        {
            int urlId = 1;
            var healthData = new HealthData { id = urlId, Weight = 75, Blood_pressure = 130, Blood_sugar = 95 };
            _HealthDataServiceMock
                .Setup(x => x.Save(It.IsAny<HealthData>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var result = await _controller.Edit(urlId, healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _HealthDataServiceMock.Verify(x => x.Save(healthData), Times.Once);
        }

        [Fact]
        public async Task Edit_Post_Should_Return_View_When_Model_Is_Invalid()
        {
            int urlId = 1;
            var healthData = new HealthData { id = urlId };
            _controller.ModelState.AddModelError("Weight", "Required");

            var result = await _controller.Edit(urlId, healthData) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result.Model);
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
            var list = (HealthData)null;
            _HealthDataServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            var result = await _controller.Delete(id) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_list_was_found()
        {
            int id = 1;
            var list = new HealthData { id = id };
            _HealthDataServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            var result = await _controller.Delete(id) as ViewResult;

            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Delete"
            );
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async Task Create_should_save_and_redirect_when_model_is_valid()
        {
            var healthData = new HealthData { id = 1, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };
            _HealthDataServiceMock
                .Setup(x => x.Save(It.IsAny<HealthData>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var result = await _controller.Create(healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _HealthDataServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Create_should_return_view_when_model_is_invalid()
        {
            var healthData = new HealthData { id = 1 };
            _controller.ModelState.AddModelError("Weight", "Required");

            var result = await _controller.Create(healthData) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(healthData, result.Model);
        }

        [Fact]
        public async Task Edit_should_save_and_redirect_when_model_is_valid()
        {
            var healthData = new HealthData { id = 1, Weight = 75, Blood_pressure = 130, Blood_sugar = 95 };

            _HealthDataServiceMock
                .Setup(x => x.Save(It.IsAny<HealthData>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var result = await _controller.Edit(healthData.id, healthData) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _HealthDataServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_does_not_match_model_id()
        {
            var healthData = new HealthData { id = 1 };
            int id = 2;

            _HealthDataServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(healthData);

            var result = await _controller.Edit(id, healthData) as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_and_redirect_when_valid_id()
        {
            int id = 1;
            _HealthDataServiceMock
                .Setup(x => x.Delete(id))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _HealthDataServiceMock.VerifyAll();
        }
    }
}
