namespace KooliProjekt.Data.Repositories
{
    public interface INutritionRepository
    {
        Task<Nutrition> Get(int id);
        Task<PagedResult<Nutrition>> List(int page, int pageSize);
        Task Save(Nutrition item);
        Task Delete(int id);
    }
}