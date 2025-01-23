using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IHealthDataService
    {
        Task<PagedResult<HealthData>> List(int page, int pageSize);
        Task<HealthData> Get(int? Id);
        Task Save(HealthData HealthData);
        Task Delete(int? Id);
        Task<bool> Includes(int Id);

    }
}