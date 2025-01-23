using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface PatientRepository
    {
        Task<PagedResult<Patient>> List(int page, int pageSize);
        Task<Patient> Get(int id);
        Task Save(Patient list);
        Task Delete(int id);
    }
}