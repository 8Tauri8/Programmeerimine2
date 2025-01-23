using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface NutrientsRepository
    {
        Task<PagedResult<Nutrients>> List(int page, int pageSize);
        Task<Nutrients> Get(int id);
        Task Save(Nutrients list);
        Task Delete(int id);
    }
}