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
            return await _context.Nutrition.GetPagedAsync(page, pageSize);
        }

        public async Task<Nutrition?> Get(int id)
        {
            var result = await _context.Nutrition.FirstOrDefaultAsync(m => m.id == id);
            return result;
        }

        public async Task Save(Nutrition nutrition)
        {
            if (nutrition.id == 0)
            {
                _context.Nutrition.Add(nutrition);
            }
            else
            {
                _context.Nutrition.Update(nutrition);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var nutrition = await _context.Nutrition.FindAsync(id);
            if (nutrition != null)
            {
                _context.Nutrition.Remove(nutrition);
                await _context.SaveChangesAsync();
            }
        }
    }
}
