using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI.Api
{
    public interface IApiClient
    {
        Task<Result<List<HealthData>>> List();
        Task<Result> Save(HealthData list);
        Task Delete(int id);
    }
}
