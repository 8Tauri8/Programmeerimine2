using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KooliProjekt.Services
{
    public class NutrientsService : INutrientsService
    {
        private readonly ApplicationDbContext _context;

        public NutrientsService(ApplicationDbContext context)
        {
            _context = context;
        }

        // List method with dynamic page size and search
        public async Task<PagedResult<Nutrients>> List(int page, int pageSize, string search = null)
        {
            var query = _context.Nutrients.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n =>
                    EF.Functions.Like(n.Name, $"%{search}%") ||
                    EF.Functions.Like(n.Sugar.ToString(), $"%{search}%") ||
                    EF.Functions.Like(n.Fat.ToString(), $"%{search}%") ||
                    EF.Functions.Like(n.Carbohydrates.ToString(), $"%{search}%"));
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        // Get method that returns null if the nutrient is not found
        public async Task<Nutrients> Get(int id)
        {
            var result = await _context.Nutrients.FirstOrDefaultAsync(m => m.id == id);
            return result;
        }

        // Save method to add or update a nutrient
        public async Task Save(Nutrients nutrient)
        {
            if (nutrient.id == 0)
            {
                // Add new nutrient if id is 0
                await _context.Nutrients.AddAsync(nutrient);
            }
            else
            {
                // Update existing nutrient if id is not 0
                _context.Nutrients.Update(nutrient);
            }

            await _context.SaveChangesAsync();
        }

        // Delete method to remove a nutrient by its id
        public async Task Delete(int id)
        {
            var nutrient = await _context.Nutrients.FindAsync(id);
            if (nutrient != null)
            {
                _context.Nutrients.Remove(nutrient);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Optional: Throw an exception if the nutrient was not found
                throw new KeyNotFoundException("Nutrient not found.");
            }
        }

        public Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search)
        {
            throw new NotImplementedException();
        }
    }
}
