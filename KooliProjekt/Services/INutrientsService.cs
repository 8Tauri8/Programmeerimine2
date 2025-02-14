using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface INutrientsService
    {
        Task<PagedResult<Nutrients>> List(int page, int pageSize, NutrientsSearch search);
        Task<Nutrients> Get(int id);
        Task Save(Nutrients nutrient);
        Task Delete(int id);
    }
}
