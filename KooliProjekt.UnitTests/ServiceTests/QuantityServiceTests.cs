using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            await DbContext.SaveChangesAsync();

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
            await DbContext.SaveChangesAsync();

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
            await DbContext.SaveChangesAsync();

            // Assert
            var savedQuantity = await DbContext.Quantity.FirstOrDefaultAsync();
            Assert.NotNull(savedQuantity);
            Assert.Equal(10, savedQuantity.Nutrients);
        }

        [Fact]
        public async Task Save_ShouldUpdateExistingQuantity()
        {
            // Arrange
            var quantity = new Quantity { Nutrients = 10, Amount = 5 };
            DbContext.Quantity.Add(quantity);
            await DbContext.SaveChangesAsync();
            var quantityId = quantity.id;
            DbContext.ChangeTracker.Clear();

            // Act
            quantity.Amount = 8;
            await _quantityService.Save(quantity);
            await DbContext.SaveChangesAsync();

            // Assert
            var updatedQuantity = await DbContext.Quantity.FindAsync(quantityId);
            Assert.NotNull(updatedQuantity);
            Assert.Equal(8, updatedQuantity.Amount);
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
            await DbContext.SaveChangesAsync();

            // Assert
            var deletedQuantity = await DbContext.Quantity.FindAsync(quantity.id);
            Assert.Null(deletedQuantity);
        }

        [Fact]
        public async Task Get_ShouldReturnDefaultQuantityWhenNotFound()
        {
            // Act
            var result = await _quantityService.Get(999);
            await DbContext.SaveChangesAsync();

            // Assert
            Assert.NotNull(result); // It should not be null, but a default Quantity object
            Assert.Equal(0, result.id); // Verify it's a default object
        }
    }
}
