using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface INutritionService
    {
        Task<PagedResult<Nutrition>> List(int page, int pageSize);
        Task<Nutrition> Get(int id);
        Task Save(Nutrition list);
        Task Delete(int id);
    }
}