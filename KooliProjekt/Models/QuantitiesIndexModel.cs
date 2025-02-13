using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class QuantitiesIndexModel
    {
        public HealthDatasSearch Search { get; set; }
        public PagedResult<Quantity> Data { get; set; }
    }
}
