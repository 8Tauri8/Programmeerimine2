using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IQuantityService
    {
        Task<PagedResult<Quantity>> List(int page, int pageSize);
        Task<Quantity> Get(int id);
        Task Save(Quantity list);
        Task Delete(int id);
    }
}