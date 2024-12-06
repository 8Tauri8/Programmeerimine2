namespace KooliProjekt.Data.Repositories
{
    public interface INutrientsRepository
    {
        Task<Nutrients> Get(int id);
        Task<PagedResult<Nutrients>> List(int page, int pageSize);
        Task Save(Nutrients item);
        Task Delete(int id);
    }
}