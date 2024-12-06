namespace KooliProjekt.Data.Repositories
{
    public interface IHealthDataRepository
    {
        Task<HealthData> Get(int id);
        Task<PagedResult<HealthData>> List(int page, int pageSize);
        Task Save(HealthData item);
        Task Delete(int id);
    }
}