using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IHealthDataService
    {
        Task<PagedResult<HealthData>> List(int page, int pageSize);
        Task<HealthData> Get(int id);
        Task Save(HealthData healthData);
        Task Delete(int id);
    }
}
