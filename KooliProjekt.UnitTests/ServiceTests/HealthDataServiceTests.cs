using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.UnitTests.ServiceTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class HealthDataServiceTests : ServiceTestBase
{
    public HealthDataServiceTests() : base() { }

    [Fact]
    public async Task Save_should_add_new_health_data()
    {
        // Arrange
        var service = new HealthDataService(DbContext);
        var healthData = new HealthData { Weight = 80, Blood_pressure = 140, Blood_sugar = 100 };

        // Clear existing data
        DbContext.HealthData.RemoveRange(DbContext.HealthData);
        await DbContext.SaveChangesAsync();

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

        // Clear any existing data in the database to avoid conflict
        DbContext.HealthData.RemoveRange(DbContext.HealthData);
        await DbContext.SaveChangesAsync();

        // Add initial health data
        var existingHealthData = new HealthData { id = 1, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };
        DbContext.HealthData.Add(existingHealthData);
        await DbContext.SaveChangesAsync(); // Save initial data

        // Update the existing record
        existingHealthData.Weight = 75;

        // Act
        await service.Save(existingHealthData);  // This should update the existing health data

        // Assert
        var updatedData = await DbContext.HealthData.FirstOrDefaultAsync(hd => hd.id == existingHealthData.id);
        Assert.NotNull(updatedData);
        Assert.Equal(existingHealthData.Weight, updatedData.Weight);  // Ensure data is updated correctly
    }

    [Fact]
    public async Task Save_should_add_new_health_data_when_not_existing()
    {
        // Arrange
        var service = new HealthDataService(DbContext);
        var healthData = new HealthData { Weight = 80, Blood_pressure = 140, Blood_sugar = 100 }; // No id set

        // Clear any existing data
        DbContext.HealthData.RemoveRange(DbContext.HealthData);
        await DbContext.SaveChangesAsync();

        // Act
        await service.Save(healthData); // This should add a new health data entry

        // Assert
        var result = await DbContext.HealthData.FirstOrDefaultAsync(hd => hd.Weight == healthData.Weight && hd.Blood_pressure == healthData.Blood_pressure && hd.Blood_sugar == healthData.Blood_sugar);
        Assert.NotNull(result);
        Assert.Equal(healthData.Weight, result.Weight);
        Assert.Equal(healthData.Blood_pressure, result.Blood_pressure);
        Assert.Equal(healthData.Blood_sugar, result.Blood_sugar);
    }


    [Fact]
    public async Task Delete_should_remove_given_health_data()
    {
        // Arrange
        var service = new HealthDataService(DbContext);
        var healthData = new HealthData { Weight = 80, Blood_pressure = 140, Blood_sugar = 100 };

        // Clear existing data before adding new data
        DbContext.HealthData.RemoveRange(DbContext.HealthData);
        await DbContext.SaveChangesAsync();

        // Add test data
        DbContext.HealthData.Add(healthData);
        await DbContext.SaveChangesAsync();  // Save initial data

        // Act
        await service.Delete(healthData.id);  // Delete the data

        // Assert
        var count = DbContext.HealthData.Count();  // Count remaining entries
        Assert.Equal(0, count);  // Ensure no entries are left
    }

    [Fact]
    public async Task Get_should_return_health_data_when_found()
    {
        // Arrange
        var service = new HealthDataService(DbContext);
        var healthData = new HealthData { Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };

        // Add data without specifying the id so it auto-generates
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
