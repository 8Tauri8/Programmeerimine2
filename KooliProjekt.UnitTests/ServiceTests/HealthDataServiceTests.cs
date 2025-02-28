using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class HealthDataServiceTests : ServiceTestBase
    {
        private readonly HealthDataService _healthDataService;

        public HealthDataServiceTests()
        {
            _healthDataService = new HealthDataService(DbContext);
        }

        [Fact]
        public async Task Save_ShouldAddNewHealthData()
        {
            // Arrange
            var healthData = new HealthData { Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f };

            // Act
            await _healthDataService.Save(healthData);
            await DbContext.SaveChangesAsync();

            // Assert
            var savedHealthData = await DbContext.HealthData.FirstOrDefaultAsync();
            Assert.NotNull(savedHealthData);
            Assert.Equal(75.5f, savedHealthData.Weight);
        }

        [Fact]
        public async Task Save_ShouldUpdateExistingHealthData()
        {
            // Arrange
            var healthData = new HealthData { Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f };
            DbContext.HealthData.Add(healthData);
            await DbContext.SaveChangesAsync();

            // Act
            healthData.Weight = 80.0f;
            await _healthDataService.Save(healthData);
            await DbContext.SaveChangesAsync();

            // Assert
            var updatedHealthData = await DbContext.HealthData.FindAsync(healthData.id);
            Assert.NotNull(updatedHealthData);
            Assert.Equal(80.0f, updatedHealthData.Weight);
        }

        [Fact]
        public async Task Get_ShouldReturnHealthDataById()
        {
            // Arrange
            var healthData = new HealthData { Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f };
            DbContext.HealthData.Add(healthData);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _healthDataService.Get(healthData.id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(75.5f, result.Weight);
        }

        [Fact]
        public async Task Get_ShouldReturnNullWhenNotFound()
        {
            // Act
            var result = await _healthDataService.Get(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveHealthData()
        {
            // Arrange
            var healthData = new HealthData { Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f };
            DbContext.HealthData.Add(healthData);
            await DbContext.SaveChangesAsync();

            // Act
            await _healthDataService.Delete(healthData.id);
            await DbContext.SaveChangesAsync();

            // Assert
            var deletedHealthData = await DbContext.HealthData.FindAsync(healthData.id);
            Assert.Null(deletedHealthData);
        }

        [Fact]
        public async Task List_ShouldReturnPagedResultsWithoutSearch()
        {
            // Arrange
            DbContext.HealthData.AddRange(new HealthData[]
            {
                new HealthData { Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f },
                new HealthData { Weight = 80.0f, Blood_pressure = 130.5f, Blood_sugar = 95.7f },
                new HealthData { Weight = 90.2f, Blood_pressure = 140.0f, Blood_sugar = 100.0f }
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _healthDataService.List(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count());
            Assert.Equal(3, result.RowCount); // Total rows in the database
        }

        [Fact]
        public async Task List_ShouldReturnPagedResultsWithSearchKeyword()
        {
            // Arrange
            DbContext.HealthData.AddRange(new HealthData[]
            {
                new HealthData { Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f },
                new HealthData { Weight = 80.0f, Blood_pressure = 130.5f, Blood_sugar = 95.7f },
                new HealthData { Weight = 90.2f, Blood_pressure = 140.0f, Blood_sugar = 100.0f }
            });
            await DbContext.SaveChangesAsync();

            var searchCriteria = new HealthDatasSearch { Keyword = "80" };

            // Act
            var result = await _healthDataService.List(1, 2, searchCriteria);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Results); // Only one record matches the keyword "80"
        }

        [Fact]
        public async Task List_ShouldHandleEmptySearchKeyword()
        {
            // Arrange
            DbContext.HealthData.AddRange(new HealthData[]
            {
                new HealthData { Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f },
                new HealthData { Weight = 80.0f, Blood_pressure = 130.5f, Blood_sugar = 95.7f },
                new HealthData { Weight = 90.2f, Blood_pressure = 140.0f, Blood_sugar = 100.0f }
            });
            await DbContext.SaveChangesAsync();

            var searchCriteria = new HealthDatasSearch { Keyword = null };

            // Act
            var result = await _healthDataService.List(1, 2, searchCriteria);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count()); // All records are returned since no filtering is applied
            Assert.Equal(3, result.RowCount);
        }

        [Fact]
        public async Task Includes_should_return_true_when_health_data_exists()
        {
            // Arrange
            var healthData = new HealthData { Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f };
            DbContext.HealthData.Add(healthData);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _healthDataService.Includes(healthData.id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Includes_should_return_false_when_health_data_does_not_exist()
        {
            // Act
            var result = await _healthDataService.Includes(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Save_ShouldAddNewHealthDataWhenExistingNotFound()
        {
            // Arrange
            var healthData = new HealthData { id = 999, Weight = 75.5f, Blood_pressure = 120.8f, Blood_sugar = 90.2f };

            // Act
            await _healthDataService.Save(healthData);
            await DbContext.SaveChangesAsync();

            // Assert
            var savedHealthData = await DbContext.HealthData.FindAsync(999);
            Assert.NotNull(savedHealthData);
            Assert.Equal(75.5f, savedHealthData.Weight);
        }
    }
}
