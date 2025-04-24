
namespace KooliProjekt.PublicAPI.Api

{
    public interface IApiClient
    {
        Task<Result<List<HealthData>>> List();
        Task Save(HealthData list);
        Task Delete(int id);
    }
}