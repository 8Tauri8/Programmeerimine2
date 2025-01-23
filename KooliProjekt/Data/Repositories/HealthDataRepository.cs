using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class HealthDataRepository : IHealthDataRepository
    {
        private readonly ApplicationDbContext _context;

        public HealthDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<HealthData>> List(int page, int pageSize)
        {
            // Rakenda leheline kuvamine (paginated)
            return await _context.HealthData.GetPagedAsync(page, pageSize);
        }

        public async Task<HealthData> Get(int id)
        {
            return await _context.HealthData.FirstOrDefaultAsync(m => m.id == id);
        }

        public async Task Save(HealthData healthData)
        {
            if (healthData.id == 0)
            {
                _context.Add(healthData);
            }
            else
            {
                _context.Update(healthData);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var healthData = await _context.HealthData.FindAsync(id);
            if (healthData != null)
            {
                _context.HealthData.Remove(healthData);
                await _context.SaveChangesAsync();
            }
        }
    }
}
