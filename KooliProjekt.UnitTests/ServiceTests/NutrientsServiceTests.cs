using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class NutrientsServiceTests : ServiceTestBase
    {
        private readonly NutrientsService _nutrientsService;

        public NutrientsServiceTests()
        {
            // Ensure a fresh in-memory database for each test run
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();

            _nutrientsService = new NutrientsService(DbContext);
        }

        [Fact]
        public async Task Save_ShouldAddNewNutrient()
        {
            // Arrange
            var nutrient = new Nutrients { Name = "Protein", Sugar = 5, Fat = 10, Carbohydrates = 20 };

            // Act
            await _nutrientsService.Save(nutrient);

            // Assert
            var savedNutrient = await DbContext.Nutrients.FirstOrDefaultAsync();
            Assert.NotNull(savedNutrient);
            Assert.Equal("Protein", savedNutrient.Name);
        }

        [Fact]
        public async Task Save_ShouldUpdateExistingNutrient()
        {
            // Arrange
            var nutrient = new Nutrients { Name = "Protein", Sugar = 5, Fat = 10, Carbohydrates = 20 };
            DbContext.Nutrients.Add(nutrient);
            await DbContext.SaveChangesAsync();
            nutrient.Fat = 15;

            // Act
            await _nutrientsService.Save(nutrient);

            // Assert
            var updatedNutrient = await DbContext.Nutrients.FindAsync(nutrient.id);
            Assert.NotNull(updatedNutrient);
            Assert.Equal(15, updatedNutrient.Fat);
        }

        [Fact]
        public async Task Get_ShouldReturnNutrientById()
        {
            // Arrange
            var nutrient = new Nutrients { Name = "Protein", Sugar = 5, Fat = 10, Carbohydrates = 20 };
            DbContext.Nutrients.Add(nutrient);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _nutrientsService.Get(nutrient.id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Protein", result.Name);
        }

        [Fact]
        public async Task Get_ShouldReturnNullWhenNotFound()
        {
            // Act
            var result = await _nutrientsService.Get(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveNutrient()
        {
            // Arrange
            var nutrient = new Nutrients { Name = "Protein", Sugar = 5, Fat = 10, Carbohydrates = 20 };
            DbContext.Nutrients.Add(nutrient);
            await DbContext.SaveChangesAsync();

            // Act
            await _nutrientsService.Delete(nutrient.id);

            // Assert
            var deletedNutrient = await DbContext.Nutrients.FindAsync(nutrient.id);
            Assert.Null(deletedNutrient);
        }

        [Fact]
        public async Task List_ShouldReturnPagedNutrients()
        {
            // Arrange
            DbContext.Nutrients.AddRange(new List<Nutrients>
            {
                new Nutrients { Name = "Protein", Sugar = 5, Fat = 10, Carbohydrates = 20 },
                new Nutrients { Name = "Fiber", Sugar = 2, Fat = 3, Carbohydrates = 15 },
                new Nutrients { Name = "Vitamin C", Sugar = 1, Fat = 0, Carbohydrates = 10 }
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _nutrientsService.List(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count());
            Assert.Equal(3, result.RowCount);
        }
    }
}