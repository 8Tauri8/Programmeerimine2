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
    public class PatientsControllerTests
    {
        private readonly Mock<IPatientService> _PatientServiceMock;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _PatientServiceMock = new Mock<IPatientService>();
            _controller = new PatientsController(_PatientServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            var page = 1;
            var data = new List<Patient>
            {
                new Patient { id = 1, Name = "Kutsu", HealthData = "Good", Nutrition = "Stable" },
                new Patient { id = 2, Name = "Asvu", HealthData = "Bad", Nutrition = "Bad" }
            };
            var pagedResult = new PagedResult<Patient>
            {
                Results = data,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 5,
                RowCount = 2
            };
            _PatientServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

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
            var list = (Patient)null;
            _PatientServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            var list = new Patient { id = id };
            _PatientServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            var patient = new Patient { id = 1, Name = "John", HealthData = "Healthy", Nutrition = "Balanced" };
            _PatientServiceMock.Setup(x => x.Save(It.IsAny<Patient>())).Returns(Task.CompletedTask).Verifiable();

            var result = await _controller.Create(patient) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _PatientServiceMock.Verify(x => x.Save(patient), Times.Once);
        }

        [Fact]
        public async Task Create_should_return_view_when_model_is_invalid()
        {
            var patient = new Patient { id = 1 };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Create(patient) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(patient, result.Model);
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
            var list = (Patient)null;
            _PatientServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            var list = new Patient { id = id };
            _PatientServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            var patient = new Patient { id = 2, Name = "John", HealthData = "Healthy", Nutrition = "Balanced" };
            _PatientServiceMock.Setup(x => x.Save(It.IsAny<Patient>())).Verifiable();

            // Act
            var result = await _controller.Edit(urlId, patient) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            _PatientServiceMock.Verify(x => x.Save(It.IsAny<Patient>()), Times.Never);
        }

        [Fact]
        public async Task Edit_Post_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            int urlId = 1;
            var patient = new Patient { id = urlId, Name = "John", HealthData = "Healthy", Nutrition = "Balanced" };
            _PatientServiceMock.Setup(x => x.Save(It.IsAny<Patient>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.Edit(urlId, patient) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _PatientServiceMock.Verify(x => x.Save(patient), Times.Once);
        }

        [Fact]
        public async Task Edit_Post_should_return_view_when_model_is_invalid()
        {
            // Arrange
            int urlId = 1;
            var patient = new Patient { id = urlId };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.Edit(urlId, patient) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patient, result.Model);
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
            var list = (Patient)null;
            _PatientServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            var list = new Patient { id = id };
            _PatientServiceMock.Setup(x => x.Get(id)).ReturnsAsync(list);

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
            _PatientServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _PatientServiceMock.Verify(x => x.Delete(id), Times.Once);
        }
    }
}
