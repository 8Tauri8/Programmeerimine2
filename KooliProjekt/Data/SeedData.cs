using KooliProjekt.Data;
using KooliProjekt.Models;
using System.Linq;

public static class SeedData
{
    public static void Generate(ApplicationDbContext dbContext)
    {
        // Kontrollige, kas HealthData tabelis on andmeid
        if (!dbContext.HealthData.Any())
        {
            // Kui ei ole, lisame vähemalt 10 terviseandmete rida
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

        // Kontrollige, kas Nutrients tabelis on andmeid
        if (!dbContext.Nutrients.Any())
        {
            // Kui ei ole, lisame vähemalt 10 toitaineandmeid
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

        // Kontrollige, kas Nutrition tabelis on andmeid
        if (!dbContext.Nutrition.Any())
        {
            // Kui ei ole, lisame vähemalt 10 toitumise andmeid
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

        // Salvestame muudatused andmebaasi
        dbContext.SaveChanges();
    }
}
