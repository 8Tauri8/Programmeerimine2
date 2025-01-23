namespace KooliProjekt.Data.Repositories
{
    public interface IQuantityRepository
    {
        Task<Quantity> Get(int id);
        Task<PagedResult<Quantity>> List(int page, int pageSize);
        Task Save(Quantity item);
        Task Delete(int id);
    }
}