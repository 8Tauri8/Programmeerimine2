using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class NutrientsService : INutrientsService
    {
        private readonly INutrientsRepository _repository;

        public NutrientsService(INutrientsRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<Nutrients>> List(int page, int pageSize)
        {
            return await _repository.List(page, pageSize);
        }

        public async Task<Nutrients> Get(int id)
        {
            var result = await _repository.Get(id);
            return result ?? new Nutrients(); // Returns a default Nutrients object if not found
        }

        public async Task Save(Nutrients nutrient)
        {
            await _repository.Save(nutrient);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
