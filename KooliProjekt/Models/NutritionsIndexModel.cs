using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class NutritionsIndexModel
    {
        public HealthDatasSearch Search { get; set; }
        public PagedResult<Nutrition> Data { get; set; }
    }
}
