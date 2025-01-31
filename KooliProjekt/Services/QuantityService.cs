using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Services
{
    public class QuantityService : IQuantityService
    {
        private readonly IQuantityRepository _repository;

        public QuantityService(IQuantityRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<Quantity>> List(int page, int pageSize)
        {
            return await _repository.List(page, pageSize);
        }

        public async Task<Quantity> Get(int id)
        {
            var result = await _repository.Get(id);
            return result ?? new Quantity(); // Returns a default Quantity if not found
        }

        public async Task Save(Quantity item)
        {
            await _repository.Save(item);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
