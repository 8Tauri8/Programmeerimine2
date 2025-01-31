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
    public class QuantityServiceTests
    {
        private readonly Mock<IQuantityRepository> _repositoryMock;
        private readonly QuantityService _service;

        public QuantityServiceTests()
        {
            // Create a mock IQuantityRepository
            var quantityList = new List<Quantity>
            {
                new Quantity { id = 1, Nutrients = 50f, Amount = 100f },
                new Quantity { id = 2, Nutrients = 75f, Amount = 150f }
            };

            // Mock the repository to return the quantity list
            _repositoryMock = new Mock<IQuantityRepository>();
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new PagedResult<Quantity>
            {
                Results = quantityList,
                RowCount = quantityList.Count,
                CurrentPage = 1,
                PageSize = 5,
                PageCount = 1
            });
            _repositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) => quantityList.FirstOrDefault(q => q.id == id));
            _repositoryMock.Setup(r => r.Save(It.IsAny<Quantity>())).Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Initialize the service with the mocked repository
            _service = new QuantityService(_repositoryMock.Object);
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
        public async Task Get_should_return_quantity_when_found()
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
        public async Task Get_should_return_empty_quantity_when_not_found()
        {
            // Arrange
            var id = 999; // ID that doesn't exist

            // Act
            var result = await _service.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.id);  // Since it returns a default Quantity if not found
        }

        [Fact]
        public async Task Save_should_add_new_quantity_when_id_is_zero()
        {
            // Arrange
            var newQuantity = new Quantity { id = 0, Nutrients = 90f, Amount = 200f };

            // Act
            await _service.Save(newQuantity);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<Quantity>()), Times.Once);
        }

        [Fact]
        public async Task Save_should_update_existing_quantity_when_id_is_not_zero()
        {
            // Arrange
            var existingQuantity = new Quantity { id = 1, Nutrients = 50f, Amount = 100f };

            // Act
            await _service.Save(existingQuantity);

            // Assert
            _repositoryMock.Verify(r => r.Save(It.IsAny<Quantity>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_remove_quantity_when_found()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Delete_should_not_remove_quantity_when_not_found()
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
