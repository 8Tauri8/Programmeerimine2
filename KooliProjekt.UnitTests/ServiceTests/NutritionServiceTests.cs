using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class NutritionServiceTests : ServiceTestBase
    {
        private readonly NutritionService _nutritionService;

        public NutritionServiceTests()
        {
            _nutritionService = new NutritionService(DbContext);
        }

        [Fact]
        public async Task Save_ShouldAddNewNutrition()
        {
            // Arrange
            var nutrition = new Nutrition { Eating_time = DateTime.Now, Nutrients = 10, Quantity = 5 };

            // Act
            await _nutritionService.Save(nutrition);
            await DbContext.SaveChangesAsync();

            // Assert
            var savedNutrition = await DbContext.Nutrition.FirstOrDefaultAsync();
            Assert.NotNull(savedNutrition);
            Assert.Equal(10, savedNutrition.Nutrients);
            Assert.Equal(5, savedNutrition.Quantity);
        }

        [Fact]
        public async Task Save_ShouldUpdateExistingNutrition()
        {
            // Arrange
            var nutrition = new Nutrition { Eating_time = DateTime.Now, Nutrients = 10, Quantity = 5 };
            DbContext.Nutrition.Add(nutrition);
            await DbContext.SaveChangesAsync();
            var nutritionId = nutrition.id; // Get the ID after saving
            DbContext.ChangeTracker.Clear(); // Detach the existing entity

            //Act
            nutrition.Nutrients = 15;
            await _nutritionService.Save(nutrition);
            await DbContext.SaveChangesAsync();

            // Assert
            var updatedNutrition = await DbContext.Nutrition.FindAsync(nutritionId);
            Assert.NotNull(updatedNutrition);
            Assert.Equal(15, updatedNutrition.Nutrients);
        }


        [Fact]
        public async Task Get_ShouldReturnNutritionById()
        {
            // Arrange
            var nutrition = new Nutrition { Eating_time = DateTime.Now, Nutrients = 10, Quantity = 5 };
            DbContext.Nutrition.Add(nutrition);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _nutritionService.Get(nutrition.id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Nutrients);
            Assert.Equal(5, result.Quantity);
        }

        [Fact]
        public async Task Get_ShouldReturnNullWhenNotFound()
        {
            // Act
            var result = await _nutritionService.Get(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveNutrition()
        {
            // Arrange
            var nutrition = new Nutrition { Eating_time = DateTime.Now, Nutrients = 10, Quantity = 5 };
            DbContext.Nutrition.Add(nutrition);
            await DbContext.SaveChangesAsync();

            // Act
            await _nutritionService.Delete(nutrition.id);
            await DbContext.SaveChangesAsync();

            // Assert
            var deletedNutrition = await DbContext.Nutrition.FindAsync(nutrition.id);
            Assert.Null(deletedNutrition);
        }

        [Fact]
        public async Task List_ShouldReturnPagedNutrition()
        {
            // Arrange
            DbContext.Nutrition.AddRange(new List<Nutrition>
            {
                new Nutrition { Eating_time = DateTime.Now, Nutrients = 10, Quantity = 5 },
                new Nutrition { Eating_time = DateTime.Now, Nutrients = 15, Quantity = 10 },
                new Nutrition { Eating_time = DateTime.Now, Nutrients = 20, Quantity = 15 }
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _nutritionService.List(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count());
            Assert.Equal(3, result.RowCount);
        }
    }
}
