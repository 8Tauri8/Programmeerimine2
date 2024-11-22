using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class NutrientsService : INutrientsService
    {
        private readonly ApplicationDbContext _context;

        public NutrientsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Nutrients>> List(int page, int pageSize)
        {
            return await _context.Nutrients.GetPagedAsync(page, 5);
        }

        public async Task<Nutrients> Get(int id)
        {
            var result = await _context.Nutrients.FirstOrDefaultAsync(m => m.id == id);
            return result ?? new Nutrients(); // Returns a default HealthData if null is found
        }

        public async Task Save(Nutrients list)
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
            var todoList = await _context.Nutrients.FindAsync(id);
            if (todoList != null)
            {
                _context.Nutrients.Remove(todoList);
                await _context.SaveChangesAsync();
            }            
        }
    }
}
