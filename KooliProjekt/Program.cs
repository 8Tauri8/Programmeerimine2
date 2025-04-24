using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KooliProjekt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string for the database
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Add DbContext with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add identity and default user store (Entity Framework)
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add controllers with views
            builder.Services.AddControllersWithViews();

            // Add services for your application
            builder.Services.AddScoped<IHealthDataService, HealthDataService>();
            builder.Services.AddScoped<INutrientsService, NutrientsService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<INutritionService, NutritionService>();
            builder.Services.AddScoped<IQuantityService, QuantityService>();

            // Enable CORS to allow Blazor WebAssembly to call this API
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy => policy.AllowAnyOrigin()   // Allow any origin
                                    .AllowAnyMethod()   // Allow any HTTP methods (GET, POST, PUT, DELETE, etc.)
                                    .AllowAnyHeader()); // Allow any headers
            });

            var app = builder.Build();

#if DEBUG
            // Seed data in development
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                SeedData.Generate(dbContext);
            }
#endif

            // Use CORS middleware
            app.UseCors("AllowAllOrigins");

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Define controller routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Map HealthDatas specific route
            app.MapControllerRoute(
                name: "healthdatas",
                pattern: "HealthDatas/{action=Index}/{id?}",
                defaults: new { controller = "HealthDatas" });

            app.MapRazorPages();

            app.Run();
        }
    }
}
