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
            return result ?? new Patient(); // Returns a default Patient if null is found
        }

        public async Task Save(Patient patient)
        {
            if (patient.id == 0)
            {
                _context.Patient.Add(patient);
            }
            else
            {
                _context.Patient.Update(patient);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
