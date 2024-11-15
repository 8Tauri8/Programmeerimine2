using KooliProjekt.Data;
using KooliProjekt.Models;
using System;
using System.Linq;

public static class SeedData
{
    public static void Generate(ApplicationDbContext dbContext)
    {
        // Seed HealthData if the table is empty
        if (!dbContext.HealthData.Any())
        {
            dbContext.HealthData.AddRange(
                new HealthData { Weight = 60, Blood_pressure = 120, Blood_sugar = 70 },
                new HealthData { Weight = 65, Blood_pressure = 115, Blood_sugar = 72 },
                new HealthData { Weight = 70, Blood_pressure = 125, Blood_sugar = 74 },
                new HealthData { Weight = 75, Blood_pressure = 130, Blood_sugar = 76 },
                new HealthData { Weight = 80, Blood_pressure = 135, Blood_sugar = 78 },
                new HealthData { Weight = 85, Blood_pressure = 140, Blood_sugar = 80 },
                new HealthData { Weight = 90, Blood_pressure = 145, Blood_sugar = 82 },
                new HealthData { Weight = 95, Blood_pressure = 150, Blood_sugar = 84 },
                new HealthData { Weight = 100, Blood_pressure = 155, Blood_sugar = 86 },
                new HealthData { Weight = 105, Blood_pressure = 160, Blood_sugar = 88 }
            );
        }

        // Seed Nutrients if the table is empty
        if (!dbContext.Nutrients.Any())
        {
            dbContext.Nutrients.AddRange(
                new Nutrients { Name = "Siim", Sugar = 75, Fat = 70, Carbohydrates = 59 },
                new Nutrients { Name = "Kati", Sugar = 55, Fat = 40, Carbohydrates = 60 },
                new Nutrients { Name = "Mati", Sugar = 65, Fat = 50, Carbohydrates = 65 },
                new Nutrients { Name = "Mari", Sugar = 80, Fat = 55, Carbohydrates = 70 },
                new Nutrients { Name = "Jüri", Sugar = 45, Fat = 30, Carbohydrates = 50 },
                new Nutrients { Name = "Liis", Sugar = 60, Fat = 65, Carbohydrates = 80 },
                new Nutrients { Name = "Tõnu", Sugar = 70, Fat = 75, Carbohydrates = 85 },
                new Nutrients { Name = "Anna", Sugar = 50, Fat = 45, Carbohydrates = 55 },
                new Nutrients { Name = "Olev", Sugar = 55, Fat = 60, Carbohydrates = 75 },
                new Nutrients { Name = "Ene", Sugar = 65, Fat = 70, Carbohydrates = 85 }
            );
        }

        // Seed Nutrition if the table is empty
        if (!dbContext.Nutrition.Any())
        {
            dbContext.Nutrition.AddRange(
                new Nutrition { Eating_time = new DateTime(2024, 12, 25), Nutrients = 12, Quantity = 10 },
                new Nutrition { Eating_time = new DateTime(2024, 12, 26), Nutrients = 22, Quantity = 12 },
                new Nutrition { Eating_time = new DateTime(2024, 12, 27), Nutrients = 32, Quantity = 11 },
                new Nutrition { Eating_time = new DateTime(2024, 12, 28), Nutrients = 43, Quantity = 13 },
                new Nutrition { Eating_time = new DateTime(2024, 12, 29), Nutrients = 55, Quantity = 14 },
                new Nutrition { Eating_time = new DateTime(2024, 12, 30), Nutrients = 67, Quantity = 15 },
                new Nutrition { Eating_time = new DateTime(2024, 12, 31), Nutrients = 71, Quantity = 16 },
                new Nutrition { Eating_time = new DateTime(2024, 12, 31), Nutrients = 32, Quantity = 17 },
                new Nutrition { Eating_time = new DateTime(2025, 01, 01), Nutrients = 93, Quantity = 18 },
                new Nutrition { Eating_time = new DateTime(2025, 01, 02), Nutrients = 12, Quantity = 19 }
            );
        }

        // Seed Patients if the table is empty
        if (!dbContext.Patient.Any())
        {
            dbContext.Patient.AddRange(
                new Patient { Name = "MARKUS VILISALU", HealthData = "good health", Nutrition = "ore carbs" },
                new Patient { Name = "Siim", HealthData = "stable", Nutrition = "high sugar" },
                new Patient { Name = "Kati", HealthData = "good health", Nutrition = "balanced" },
                new Patient { Name = "Mati", HealthData = "average", Nutrition = "high carbs" },
                new Patient { Name = "Mari", HealthData = "good health", Nutrition = "low fat" },
                new Patient { Name = "Tõnu", HealthData = "excellent", Nutrition = "high protein" },
                new Patient { Name = "Liis", HealthData = "good health", Nutrition = "balanced" },
                new Patient { Name = "Jüri", HealthData = "high stress", Nutrition = "low sugar" },
                new Patient { Name = "Olev", HealthData = "good health", Nutrition = "medium carb" },
                new Patient { Name = "Ene", HealthData = "healthy", Nutrition = "low sugar" }
            );
        }

        // Seed Quantity if the table is empty (assuming 'Quantity' refers to Worker related data)
        if (!dbContext.Quantity.Any())
        {
            dbContext.Quantity.AddRange(
                new Quantity { Nutrients = 25, Amount = 2 },
                new Quantity { Nutrients = 37, Amount = 4 },
                new Quantity { Nutrients = 54, Amount = 3 },
                new Quantity { Nutrients = 62, Amount = 7 },
                new Quantity { Nutrients = 78, Amount = 1 },
                new Quantity { Nutrients = 99, Amount = 2 },
                new Quantity { Nutrients = 56, Amount = 5 },
                new Quantity { Nutrients = 21, Amount = 8 },
                new Quantity { Nutrients = 42, Amount = 9 },
                new Quantity { Nutrients = 83, Amount = 6 }
            );
        }

        // Save all changes to the database
        dbContext.SaveChanges();
    }
}
