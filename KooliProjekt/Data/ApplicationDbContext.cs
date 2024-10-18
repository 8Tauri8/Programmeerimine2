using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Health_data> Health_data { get; set; }
        public DbSet<Quantity> Quantity { get; set; }
        public DbSet<Nutrients> Nutrients { get; set; }

    }
}