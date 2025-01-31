using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class NutritionServiceTests
    {
        private readonly Mock<INutritionRepository> _repositoryMock;
        private readonly NutritionService _service;

        public NutritionServiceTests()
        {
            // Create a mock INutritionRepository
            var nutritionList = new List<Nutrition>
            {
                new Nutrition { id = 1, Eating_time = DateTime.Now, Nutrients = 50.5f, Quantity = 200 },
                new Nutrition { id = 2, Eating_time = DateTime.Now.AddHours(1), Nutrients = 60.0f, Quantity = 250 }
            };

            // Mock the repository to return the nutrition list
            _repositoryMock = new Mock<INutritionRepository>();
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new PagedResult<Nutrition>
            {
                Results = nutritionList,
                RowCount = nutritionList.Count,
                CurrentPage = 1,
                PageSize = 5,
                PageCount = 1
            });
            _repositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) => nutritionList.FirstOrDefault(n => n.id == id));
            _repositoryMock.Setup(r => r.Save(It.IsAny<Nutrition>())).Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Initialize the service with the mocked repository
            _service = new NutritionService(_repositoryMock.Object);
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
        public async Task Get_should_return_nutrition_when_found()
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
        public async Task Get_should_return_empty_nutrition_when_not_found()
        {
            // Arrange
            var id = 999; // ID that doesn't exist

            // Act
            var result = await _service.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.id);  // Since it returns a default Nutrition if not found
        }

        [Fact]
        public async Task Save_should_add_new_nutrition_when_id_is_zero()
        {
            // Arrange
            var newNutrition = new Nutrition { id = 0, Eating_time = DateTime.Now, Nutrients = 30.0f, Quantity = 100 };

            // Act
            await _service.Save(newNutrition);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<Nutrition>()), Times.Once);
        }

        [Fact]
        public async Task Save_should_update_existing_nutrition_when_id_is_not_zero()
        {
            // Arrange
            var existingNutrition = new Nutrition { id = 1, Eating_time = DateTime.Now, Nutrients = 50.5f, Quantity = 200 };

            // Act
            await _service.Save(existingNutrition);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<Nutrition>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_remove_nutrition_when_found()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_not_remove_nutrition_when_not_found()
        {
            // Arrange
            var id = 999; // ID that doesn't exist

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never); // Verify Delete was never called
        }
    }
}
