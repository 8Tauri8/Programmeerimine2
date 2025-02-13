// Models/NutrientsIndexModel.cs
using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class NutrientsIndexModel
    {
        public NutrientsSearch Search { get; set; }
        public PagedResult<Nutrients> Data { get; set; }
    }
}
