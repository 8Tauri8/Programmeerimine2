using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class QuantityRepository : IQuantityRepository
    {
        private readonly ApplicationDbContext _context;

        public QuantityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Quantity>> List(int page, int pageSize)
        {
            var query = _context.Quantity.AsQueryable();
            var count = await query.CountAsync();

            var items = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedResult<Quantity>
            {
                Results = items,
                RowCount = count,
                CurrentPage = page,
                PageSize = pageSize,
                PageCount = (int)Math.Ceiling((double)count / pageSize)
            };
        }

        public async Task<Quantity> Get(int id)
        {
            return await _context.Quantity.FirstOrDefaultAsync(q => q.id == id);
        }

        public async Task Save(Quantity item)
        {
            if (item.id == 0)
            {
                await _context.Quantity.AddAsync(item);
            }
            else
            {
                _context.Quantity.Update(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _context.Quantity.FindAsync(id);
            if (item != null)
            {
                _context.Quantity.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
