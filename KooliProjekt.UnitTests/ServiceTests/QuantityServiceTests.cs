using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class QuantityServiceTests : ServiceTestBase
    {
        private readonly QuantityService _quantityService;

        public QuantityServiceTests()
        {
            _quantityService = new QuantityService(DbContext);
        }

        [Fact]
        public async Task List_ShouldReturnPagedResult()
        {
            // Arrange
            DbContext.Quantity.AddRange(new List<Quantity>
            {
                new Quantity { Nutrients = 10, Amount = 5 },
                new Quantity { Nutrients = 15, Amount = 10 }
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _quantityService.List(1, 5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count());
        }

        [Fact]
        public async Task Get_ShouldReturnQuantityById()
        {
            // Arrange
            var quantity = new Quantity { Nutrients = 10, Amount = 5 };
            DbContext.Quantity.Add(quantity);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _quantityService.Get(quantity.id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Nutrients);
        }

        [Fact]
        public async Task Save_ShouldAddNewQuantity()
        {
            // Arrange
            var quantity = new Quantity { Nutrients = 10, Amount = 5 };

            // Act
            await _quantityService.Save(quantity);

            // Assert
            var savedQuantity = await DbContext.Quantity.FirstOrDefaultAsync();
            Assert.NotNull(savedQuantity);
            Assert.Equal(10, savedQuantity.Nutrients);
        }

        [Fact]
        public async Task Delete_ShouldRemoveQuantity()
        {
            // Arrange
            var quantity = new Quantity { Nutrients = 10, Amount = 5 };
            DbContext.Quantity.Add(quantity);
            await DbContext.SaveChangesAsync();

            // Act
            await _quantityService.Delete(quantity.id);

            // Assert
            var deletedQuantity = await DbContext.Quantity.FindAsync(quantity.id);
            Assert.Null(deletedQuantity);
        }
    }
}
