using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class HealthDataService : IHealthDataService
    {
        private readonly ApplicationDbContext _context;

        public HealthDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<HealthData>> List(int page, int pageSize)
        {
            // Directly use ApplicationDbContext to get paginated health data
            return await _context.HealthData.GetPagedAsync(page, pageSize);
        }

        public async Task<HealthData> Get(int id)
        {
            // Directly retrieve a HealthData record by id from ApplicationDbContext
            var result = await _context.HealthData.FirstOrDefaultAsync(m => m.id == id);
            return result ?? new HealthData(); // Return default HealthData if not found
        }

        public async Task Save(HealthData healthData)
        {
            if (healthData.id == 0)
            {
                _context.Add(healthData);  // Add new health data if id is 0
            }
            else
            {
                _context.Update(healthData);  // Update existing health data
            }

            await _context.SaveChangesAsync();  // Save changes to the database
        }

        public async Task Delete(int id)
        {
            var healthData = await _context.HealthData.FindAsync(id);
            if (healthData != null)
            {
                _context.HealthData.Remove(healthData);  // Remove health data if found
                await _context.SaveChangesAsync();  // Save changes to the database
            }
        }
    }
}
