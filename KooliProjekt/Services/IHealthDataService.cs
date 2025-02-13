using KooliProjekt.Data;
using KooliProjekt.Search; // Make sure this is included

namespace KooliProjekt.Services
{
    public interface IHealthDataService
    {
        Task<PagedResult<HealthData>> List(int page, int pageSize, HealthDatasSearch search = null);
        Task<HealthData> Get(int? Id);
        Task Save(HealthData HealthData);
        Task Delete(int? Id);
        Task<bool> Includes(int Id);
    }
}
