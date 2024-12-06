using KooliProjekt.Data;
using KooliProjekt.Services;
namespace KooliProjekt.Services
{
    public interface IHealthRepository
    {
        Task<PagedResult<HealthData>> List(int page, int pageSize);
        Task<HealthData> Get(int id);
        Task Save(HealthData list);
        Task Delete(int id);
    }
}