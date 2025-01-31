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

        public async Task Save(HealthData HealthData)
        {
            if (HealthData.id == 0)
            {
                _context.HealthData.Add(HealthData);
            }
            else
            {
                _context.HealthData.Update(HealthData);
            }
            await _context.SaveChangesAsync();

        }
    }
}