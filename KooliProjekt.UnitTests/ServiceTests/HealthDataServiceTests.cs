using KooliProjekt.Data;
using KooliProjekt.Services;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class HealthDataServiceTests : ServiceTestBase
    {
        public HealthDataServiceTests() : base() { }

        [Fact]
        public async Task Save_should_add_new_health_data()
        {
            // Arrange
            var service = new HealthDataService(DbContext);
            var healthData = new HealthData { Weight = 80, Blood_pressure = 140, Blood_sugar = 100 };

            // Act
            await service.Save(healthData);

            // Assert
            var count = DbContext.HealthData.Count();
            var result = DbContext.HealthData.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(healthData.Weight, result.Weight);
            Assert.Equal(healthData.Blood_pressure, result.Blood_pressure);
            Assert.Equal(healthData.Blood_sugar, result.Blood_sugar);
        }

        [Fact]
        public async Task Save_should_update_existing_health_data()
        {
            // Arrange
            var service = new HealthDataService(DbContext);
            var existingHealthData = new HealthData { id = 1, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };
            DbContext.HealthData.Add(existingHealthData);
            await DbContext.SaveChangesAsync();

            existingHealthData.Weight = 75; // Update some fields

            // Act
            await service.Save(existingHealthData);

            // Assert
            var updatedData = DbContext.HealthData.FirstOrDefault(hd => hd.id == existingHealthData.id);
            Assert.NotNull(updatedData);
            Assert.Equal(existingHealthData.Weight, updatedData.Weight);
        }

        [Fact]
        public async Task Delete_should_remove_given_health_data()
        {
            // Arrange
            var service = new HealthDataService(DbContext);
            var healthData = new HealthData { Weight = 80, Blood_pressure = 140, Blood_sugar = 100 };
            DbContext.HealthData.Add(healthData);
            await DbContext.SaveChangesAsync();

            // Act
            await service.Delete(healthData.id);

            // Assert
            var count = DbContext.HealthData.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_health_data_when_found()
        {
            // Arrange
            var service = new HealthDataService(DbContext);
            var healthData = new HealthData { id = 1, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };
            DbContext.HealthData.Add(healthData);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await service.Get(healthData.id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(healthData.id, result.id);
            Assert.Equal(healthData.Weight, result.Weight);
        }

        [Fact]
        public async Task Get_should_return_null_when_not_found()
        {
            // Arrange
            var service = new HealthDataService(DbContext);

            // Act
            var result = await service.Get(999); // ID that doesn't exist

            // Assert
            Assert.Null(result);
        }
    }
}
