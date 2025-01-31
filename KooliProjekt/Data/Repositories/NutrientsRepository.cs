using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Data.Repositories
{
    public class NutrientsRepository : INutrientsRepository
    {
        private readonly ApplicationDbContext _context;

        public NutrientsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Nutrients>> List(int page, int pageSize)
        {
            var query = _context.Nutrients.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedResult<Nutrients>
            {
                Results = items,
                RowCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                PageCount = (int)System.Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<Nutrients> Get(int id)
        {
            return await _context.Nutrients.FirstOrDefaultAsync(n => n.id == id);
        }

        public async Task Save(Nutrients nutrient)
        {
            if (nutrient.id == 0)
            {
                await _context.Nutrients.AddAsync(nutrient);
            }
            else
            {
                _context.Nutrients.Update(nutrient);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var nutrient = await _context.Nutrients.FindAsync(id);
            if (nutrient != null)
            {
                _context.Nutrients.Remove(nutrient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
