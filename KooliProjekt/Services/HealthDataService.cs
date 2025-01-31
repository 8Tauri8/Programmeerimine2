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

        public async Task Delete(int? Id)
        {
            var healthData = await _context.HealthData.FindAsync(Id);
            if (healthData != null)
            {
                _context.HealthData.Remove(healthData);  // Remove the entity from DbContext
                await _context.SaveChangesAsync();      // Save changes to the database
            }
        }

        public async Task<HealthData> Get(int? Id)
        {
            return await _context.HealthData.FindAsync(Id);
        }

        public async Task<bool> Includes(int Id)
        {
            return await _context.HealthData.AnyAsync(c => c.id == Id);
        }

        public Task<PagedResult<HealthData>> List(int page, int pageSize)
        {
            return _context.HealthData.GetPagedAsync(page, pageSize);
        }

        public async Task Save(HealthData healthData)
        {
            if (healthData.id == 0)
            {
                _context.HealthData.Add(healthData); // Add new entity if ID is 0
            }
            else
            {
                var existingHealthData = await _context.HealthData.FindAsync(healthData.id);
                if (existingHealthData != null)
                {
                    // Explicitly set the entity to modified state
                    _context.Entry(existingHealthData).State = EntityState.Modified;
                }
                else
                {
                    // If entity doesn't exist, update it (add it as new, for this case)
                    _context.HealthData.Update(healthData);
                }
            }
            await _context.SaveChangesAsync(); // Save changes to the database
        }
    }
}
