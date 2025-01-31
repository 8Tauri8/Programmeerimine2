using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class HealthDataServiceTests
{
    private readonly HealthDataService _service;
    private readonly ApplicationDbContext _context;

    public HealthDataServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb") // Ensure this is using the correct package
            .Options;

        _context = new ApplicationDbContext(options);
        _service = new HealthDataService(_context);
    }

    [Fact]
    public async Task Save_AddsHealthData_WhenIdIsZero()
    {
        var healthData = new HealthData { id = 0, Weight = 70, Blood_pressure = 120, Blood_sugar = 90 };

        await _service.Save(healthData);

        var savedData = await _context.HealthData.FindAsync(healthData.id);
        Assert.NotNull(savedData);
        Assert.Equal(healthData.Weight, savedData.Weight);
    }

    [Fact]
    public async Task Save_UpdatesHealthData_WhenIdIsNotZero()
    {
        var healthData = new HealthData { id = 1, Weight = 80, Blood_pressure = 130, Blood_sugar = 95 };
        _context.HealthData.Add(healthData);
        await _context.SaveChangesAsync();

        healthData.Weight = 85; // Update value
        await _service.Save(healthData);

        var updatedData = await _context.HealthData.FindAsync(healthData.id);
        Assert.NotNull(updatedData);
        Assert.Equal(85, updatedData.Weight); // Verify the update was successful
    }

    [Fact]
    public async Task Delete_DeletesHealthData_WhenExists()
    {
        var healthData = new HealthData { id = 1, Weight = 75, Blood_pressure = 125, Blood_sugar = 100 };
        _context.HealthData.Add(healthData);
        await _context.SaveChangesAsync();

        await _service.Delete(healthData.id);

        var deletedData = await _context.HealthData.FindAsync(healthData.id);
        Assert.Null(deletedData); // Should be null as it's deleted
    }

    [Fact]
    public async Task Get_ReturnsHealthData_WhenExists()
    {
        var healthData = new HealthData { id = 1, Weight = 72, Blood_pressure = 110, Blood_sugar = 85 };
        _context.HealthData.Add(healthData);
        await _context.SaveChangesAsync();

        var result = await _service.Get(healthData.id);

        Assert.NotNull(result);
        Assert.Equal(healthData.Weight, result.Weight);
    }

    [Fact]
    public async Task Includes_ReturnsTrue_WhenHealthDataExists()
    {
        var healthData = new HealthData { id = 1, Weight = 70, Blood_pressure = 115, Blood_sugar = 90 };
        _context.HealthData.Add(healthData);
        await _context.SaveChangesAsync();

        var result = await _service.Includes(healthData.id);

        Assert.True(result);
    }
}
