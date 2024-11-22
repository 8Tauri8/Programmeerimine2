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
            return await _context.HealthData.GetPagedAsync(page, 5);
        }

        public async Task<HealthData> Get(int id)
        {
            var result = await _context.HealthData.FirstOrDefaultAsync(m => m.id == id);
            return result ?? new HealthData(); // Returns a default HealthData if null is found
        }


        public async Task Save(HealthData list)
        {
            if(list.id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var todoList = await _context.HealthData.FindAsync(id);
            if (todoList != null)
            {
                _context.HealthData.Remove(todoList);
                await _context.SaveChangesAsync();
            }            
        }
    }
}
