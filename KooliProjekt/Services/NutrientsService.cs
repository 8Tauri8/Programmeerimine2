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

        // List method with dynamic page size
        public async Task<PagedResult<Nutrients>> List(int page, int pageSize)
        {
            return await _context.Nutrients.GetPagedAsync(page, pageSize); // Use dynamic pageSize
        }

        // Get method that returns null if the nutrient is not found
        public async Task<Nutrients> Get(int id)
        {
            var result = await _context.Nutrients.FirstOrDefaultAsync(m => m.id == id);
            return result; // Return null if not found
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
    }
}
