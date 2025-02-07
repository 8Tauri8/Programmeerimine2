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
                _context.HealthData.Remove(healthData);  
                await _context.SaveChangesAsync();      
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
                // Add a new health data entry when the id is 0 (not yet saved in the DB)
                _context.HealthData.Add(healthData);
            }
            else
            {
                var existingHealthData = await _context.HealthData.FindAsync(healthData.id);

                if (existingHealthData != null)
                {
                    // If it exists, update the entity
                    _context.Entry(existingHealthData).State = EntityState.Modified;
                }
                else
                {
                    _context.HealthData.Add(healthData);
                }
            }

            await _context.SaveChangesAsync(); 
        }

    }
}
