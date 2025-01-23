using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class QuantityService : IQuantityRepository
    {
        private readonly ApplicationDbContext _context;

        public QuantityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Quantity>> List(int page, int pageSize)
        {
            return await _context.Quantity.GetPagedAsync(page, 5);
        }

        public async Task<Quantity> Get(int id)
        {
            var result = await _context.Quantity.FirstOrDefaultAsync(m => m.id == id);
            return result ?? new Quantity(); // Returns a default HealthData if null is found
        }

        public async Task Save(Quantity list)
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
            var todoList = await _context.Quantity.FindAsync(id);
            if (todoList != null)
            {
                _context.Quantity.Remove(todoList);
                await _context.SaveChangesAsync();
            }            
        }
    }
}
