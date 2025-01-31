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
    public class NutrientsServiceTests
    {
        private readonly Mock<INutrientsRepository> _repositoryMock;
        private readonly NutrientsService _service;

        public NutrientsServiceTests()
        {
            // Create a mock INutrientsRepository
            var nutrientsList = new List<Nutrients>
            {
                new Nutrients { id = 1, Name = "Vitamin C", Sugar = 0, Fat = 0, Carbohydrates = 0 },
                new Nutrients { id = 2, Name = "Iron", Sugar = 0, Fat = 0, Carbohydrates = 0 }
            };

            // Mock the repository to return the nutrients list
            _repositoryMock = new Mock<INutrientsRepository>();
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new PagedResult<Nutrients>
            {
                Results = nutrientsList,
                RowCount = nutrientsList.Count,
                CurrentPage = 1,
                PageSize = 5,
                PageCount = 1
            });
            _repositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) => nutrientsList.FirstOrDefault(n => n.id == id));
            _repositoryMock.Setup(r => r.Save(It.IsAny<Nutrients>())).Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Initialize the service with the mocked repository
            _service = new NutrientsService(_repositoryMock.Object);
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
        public async Task Get_should_return_nutrient_when_found()
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
        public async Task Get_should_return_empty_nutrient_when_not_found()
        {
            // Arrange
            var id = 999; // ID that doesn't exist

            // Act
            var result = await _service.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.id);  // Since it returns a default Nutrients if not found
        }

        [Fact]
        public async Task Save_should_add_new_nutrient_when_id_is_zero()
        {
            // Arrange
            var newNutrient = new Nutrients { id = 0, Name = "Calcium", Sugar = 0, Fat = 0, Carbohydrates = 0 };

            // Act
            await _service.Save(newNutrient);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<Nutrients>()), Times.Once);
        }

        [Fact]
        public async Task Save_should_update_existing_nutrient_when_id_is_not_zero()
        {
            // Arrange
            var existingNutrient = new Nutrients { id = 1, Name = "Vitamin C", Sugar = 0, Fat = 0, Carbohydrates = 0 };

            // Act
            await _service.Save(existingNutrient);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<Nutrients>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_remove_nutrient_when_found()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_not_remove_nutrient_when_not_found()
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
