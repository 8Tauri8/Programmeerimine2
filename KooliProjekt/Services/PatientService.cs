using KooliProjekt.Data;
using KooliProjekt.Search;
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

        public async Task<PagedResult<Patient>> List(int page, int pageSize, PatientsSearch search)
        {
            var query = _context.Patient.AsQueryable();

            if (!string.IsNullOrEmpty(search?.Name))
            {
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{search.Name}%"));
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        public async Task<Patient> Get(int id)
        {
            return await _context.Patient.FirstOrDefaultAsync(m => m.id == id) ?? new Patient();
        }

        public async Task Save(Patient patient)
        {
            if (patient.id == 0)
            {
                _context.Patient.Add(patient);
            }
            else
            {
                _context.Entry(patient).State = EntityState.Modified;
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
