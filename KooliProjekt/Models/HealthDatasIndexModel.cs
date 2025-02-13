using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class HealthDatasIndexModel
    {
        public HealthDatasSearch Search { get; set; }
        public PagedResult<HealthData> Data { get; set; }
    }
}
