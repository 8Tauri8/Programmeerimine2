using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;

namespace KooliProjekt.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PagedResult<Patient>> List(int page, int pageSize)
        {
            return await _patientRepository.List(page, pageSize);
        }

        public async Task<Patient> Get(int id)
        {
            var result = await _patientRepository.Get(id);
            return result ?? new Patient(); // Return default Patient if not found
        }

        public async Task Save(Patient patient)
        {
            await _patientRepository.Save(patient);
        }

        public async Task Delete(int id)
        {
            await _patientRepository.Delete(id);
        }
    }
}
