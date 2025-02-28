using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class NutrientsIndexModel
    {
        public PagedResult<Nutrients> Data { get; set; }
        public NutrientsSearch Search { get; set; }
    }
}
