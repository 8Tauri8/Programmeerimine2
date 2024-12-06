using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class HealthDataService : IHealthDataRepository
    {
        private readonly IHealthDataRepository _healthDataRepository;

        public HealthDataService(IHealthDataRepository healthDataRepository)
        {
            _healthDataRepository = healthDataRepository;
        }

        public async Task<PagedResult<HealthData>> List(int page, int pageSize)
        {
            return await _healthDataRepository.List(page, pageSize); // Kasutame repository meetodit
        }

        public async Task<HealthData> Get(int id)
        {
            return await _healthDataRepository.Get(id); // Kasutame repository meetodit
        }

        public async Task Save(HealthData healthData)
        {
            await _healthDataRepository.Save(healthData); // Kasutame repository meetodit
        }

        public async Task Delete(int id)
        {
            await _healthDataRepository.Delete(id); // Kasutame repository meetodit
        }
    }
}
