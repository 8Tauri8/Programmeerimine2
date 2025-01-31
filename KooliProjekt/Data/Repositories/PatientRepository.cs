using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Patient>> List(int page, int pageSize)
        {
            var patients = await _context.Patient
                                         .Skip((page - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            var rowCount = await _context.Patient.CountAsync();

            return new PagedResult<Patient>
            {
                Results = patients,
                RowCount = rowCount,
                CurrentPage = page,
                PageSize = pageSize,
                PageCount = (int)Math.Ceiling(rowCount / (double)pageSize)
            };
        }

        public async Task<Patient> Get(int id)
        {
            return await _context.Patient.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task Save(Patient patient)
        {
            if (patient.id == 0)
            {
                await _context.Patient.AddAsync(patient);
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
