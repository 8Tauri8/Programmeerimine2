using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<HealthData> HealthData { get; set; }
        public DbSet<Quantity> Quantity { get; set; }
        public DbSet<Nutrients> Nutrients { get; set; }
        public DbSet<KooliProjekt.Data.Nutrition> Nutrition { get; set; } = default!;

    }
}