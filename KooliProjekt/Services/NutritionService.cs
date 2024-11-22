using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class NutritionService : INutritionService
    {
        private readonly ApplicationDbContext _context;

        public NutritionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Nutrition>> List(int page, int pageSize)
        {
            return await _context.Nutrition.GetPagedAsync(page, 5);
        }

        public async Task<Nutrition> Get(int id)
        {
            var result = await _context.Nutrition.FirstOrDefaultAsync(m => m.id == id);
            return result ?? new Nutrition(); // Returns a default HealthData if null is found
        }

        public async Task Save(Nutrition list)
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
            var todoList = await _context.Nutrition.FindAsync(id);
            if (todoList != null)
            {
                _context.Nutrition.Remove(todoList);
                await _context.SaveChangesAsync();
            }            
        }
    }
}
