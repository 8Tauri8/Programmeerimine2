using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

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
        public async Task List_ShouldReturnPagedResult()
        {
            // Arrange
            DbContext.Nutrition.AddRange(new List<Nutrition>
            {
                new Nutrition { Eating_time = DateTime.Now, Nutrients = 10, Quantity = 5 },
                new Nutrition { Eating_time = DateTime.Now, Nutrients = 15, Quantity = 10 }
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _nutritionService.List(1, 5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count());
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
        }

        [Fact]
        public async Task Save_ShouldAddNewNutrition()
        {
            // Arrange
            var nutrition = new Nutrition { Eating_time = DateTime.Now, Nutrients = 10, Quantity = 5 };

            // Act
            await _nutritionService.Save(nutrition);

            // Assert
            var savedNutrition = await DbContext.Nutrition.FirstOrDefaultAsync();
            Assert.NotNull(savedNutrition);
            Assert.Equal(10, savedNutrition.Nutrients);
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

            // Assert
            var deletedNutrition = await DbContext.Nutrition.FindAsync(nutrition.id);
            Assert.Null(deletedNutrition);
        }
    }
}
