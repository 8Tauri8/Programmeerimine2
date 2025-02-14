using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class QuantitiesIndexModel
    {
        public QuantitiesSearch Search { get; set; }
        public PagedResult<Quantity> Data { get; set; }
    }
}
