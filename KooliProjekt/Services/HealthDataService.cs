using KooliProjekt.Data.Repositories;
using KooliProjekt.Data;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class HealthDataService : IHealthDataService
    {
        private readonly IHealthDataRepository _repository;

        public HealthDataService(IHealthDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<HealthData>> List(int page, int pageSize)
        {
            // Use repository to get paginated health data
            return await _repository.List(page, pageSize);
        }

        public async Task<HealthData> Get(int id)
        {
            // Use repository to retrieve a HealthData record by id
            var result = await _repository.Get(id);
            return result ?? new HealthData(); // Return default HealthData if not found
        }

        public async Task Save(HealthData healthData)
        {
            // Use repository to add or update health data
            await _repository.Save(healthData);
        }

        public async Task Delete(int id)
        {
            var healthData = await _repository.Get(id); // Get the health data by id
            if (healthData != null)
            {
                await _repository.Delete(id); // Only delete if health data is found
            }
        }
    }
}
