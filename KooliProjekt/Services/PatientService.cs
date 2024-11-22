using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Patient>> List(int page, int pageSize)
        {
            return await _context.Patient.GetPagedAsync(page, 5);
        }

        public async Task<Patient> Get(int id)
        {
            var result = await _context.Patient.FirstOrDefaultAsync(m => m.id == id);
            return result ?? new Patient(); // Returns a default HealthData if null is found
        }

        public async Task Save(Patient list)
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
            var todoList = await _context.Patient.FindAsync(id);
            if (todoList != null)
            {
                _context.Patient.Remove(todoList);
                await _context.SaveChangesAsync();
            }            
        }
    }
}
