using KooliProjekt.Data;



    public static class SeedData
    {
        // SeedData genereerimise meetod
        public static void Generate(ApplicationDbContext dbContext)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            dbContext.SaveChanges(); // Salvestame muudatused
        }
    }
