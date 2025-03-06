using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IPatientService
    {
        Task<PagedResult<Patient>> List(int page, int pageSize, PatientsSearch search);
        Task<Patient> Get(int id);
        Task Save(Patient patient);
        Task Delete(int id);
    }
}
