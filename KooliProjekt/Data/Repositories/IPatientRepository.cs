namespace KooliProjekt.Data.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> Get(int id);
        Task<PagedResult<Patient>> List(int page, int pageSize);
        Task Save(Patient item);
        Task Delete(int id);
    }
}