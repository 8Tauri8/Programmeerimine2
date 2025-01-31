using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Services
{
    public class NutritionService : INutritionService
    {
        private readonly INutritionRepository _nutritionRepository;

        // Constructor injected with the repository instead of DbContext
        public NutritionService(INutritionRepository nutritionRepository)
        {
            _nutritionRepository = nutritionRepository;
        }

        public async Task<PagedResult<Nutrition>> List(int page, int pageSize)
        {
            return await _nutritionRepository.List(page, pageSize);
        }

        public async Task<Nutrition> Get(int id)
        {
            var result = await _nutritionRepository.Get(id);
            return result ?? new Nutrition(); // Return default Nutrition if not found
        }

        public async Task Save(Nutrition nutrition)
        {
            await _nutritionRepository.Save(nutrition);
        }

        public async Task Delete(int id)
        {
            await _nutritionRepository.Delete(id);
        }
    }
}
