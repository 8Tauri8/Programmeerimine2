using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface QuantityRepository
    {
        Task<Quantity> Get(int id);
        Task<PagedResult<Quantity>> List(int page, int pageSize);
        Task Save(Quantity item);
        Task Delete(int id);
    }
}