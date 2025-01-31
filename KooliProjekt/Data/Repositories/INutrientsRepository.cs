using System.Threading.Tasks;
using System.Collections.Generic;

namespace KooliProjekt.Data.Repositories
{
    public interface INutrientsRepository
    {
        Task<PagedResult<Nutrients>> List(int page, int pageSize);
        Task<Nutrients> Get(int id);
        Task Save(Nutrients nutrient);
        Task Delete(int id);
    }
}
