using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class HealthDataServiceTests
    {
        private readonly Mock<IHealthDataRepository> _repositoryMock;
        private readonly HealthDataService _service;

        public HealthDataServiceTests()
        {
            // Create a mock IHealthDataRepository
            var healthDataList = new List<HealthData>
            {
                new HealthData { id = 1, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 },
                new HealthData { id = 2, Weight = 75, Blood_pressure = 130, Blood_sugar = 95 }
            };

            // Mock the repository to return the health data list
            _repositoryMock = new Mock<IHealthDataRepository>();
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new PagedResult<HealthData>
            {
                Results = healthDataList,
                RowCount = healthDataList.Count,
                CurrentPage = 1,
                PageSize = 5,
                PageCount = 1
            });
            _repositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) => healthDataList.FirstOrDefault(hd => hd.id == id));
            _repositoryMock.Setup(r => r.Save(It.IsAny<HealthData>())).Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Initialize the service with the mocked repository
            _service = new HealthDataService(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_paginated_data()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;

            // Act
            var result = await _service.List(page, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.RowCount);
            Assert.Equal(page, result.CurrentPage);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(2, result.Results.Count());
        }

        [Fact]
        public async Task Get_should_return_health_data_when_found()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _service.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.id);
        }

        [Fact]
        public async Task Get_should_return_empty_health_data_when_not_found()
        {
            // Arrange
            var id = 999; // ID that doesn't exist

            // Act
            var result = await _service.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.id);  // Since it returns a default HealthData if not found
        }

        [Fact]
        public async Task Save_should_add_new_health_data_when_id_is_zero()
        {
            // Arrange
            var newHealthData = new HealthData { id = 0, Weight = 80, Blood_pressure = 140, Blood_sugar = 100 };

            // Act
            await _service.Save(newHealthData);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<HealthData>()), Times.Once);
        }

        [Fact]
        public async Task Save_should_update_existing_health_data_when_id_is_not_zero()
        {
            // Arrange
            var existingHealthData = new HealthData { id = 1, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };

            // Act
            await _service.Save(existingHealthData);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<HealthData>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_remove_health_data_when_found()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_not_remove_health_data_when_not_found()
        {
            // Arrange
            var id = 999; // ID that doesn't exist

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
        }
    }
}
