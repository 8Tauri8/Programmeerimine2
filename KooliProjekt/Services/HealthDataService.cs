// File: Services/FoodChartService.cs

using KooliProjekt.Data;

using KooliProjekt.Data.Repositories;

using KooliProjekt.Models;

using System.Threading.Tasks;

namespace KooliProjekt.Services

{

    public class HealthDataService : IHealthDataService

    {

        private readonly IHealthDataRepository _IHealthDataRepository;

        // Constructor injection

        public HealthDataService(IHealthDataRepository HealthDataRepository)

        {

            _HealthDataRepository = HealthDataRepository;

        }

        // List method that gets paginated results from repository

        public async Task<PagedResult<HealthData>> List(int page, int pageSize)

        {

            return await _HealthDataRepository.HealthData.GetPagedAsync(page, 5);

        }

        // Method to get a specific FoodChart by ID

        public async Task<HealthData> Get(int id)

        {

            return await _HealthDataRepository.Get(id);

        }

        // Save method to insert or update a FoodChart

        public async Task Save(HealthData foodChart)

        {

            await _HealthDataRepository.Save(foodChart);

        }

        // Delete method to remove a FoodChart by ID

        public async Task Delete(int id)

        {

            await _HealthDataRepository.Delete(id);

        }

    }

}

