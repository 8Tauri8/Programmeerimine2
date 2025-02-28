using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class NutrientsIndexModel
    {
        /// <summary>
        /// Contains the paginated list of nutrients to be displayed.
        /// </summary>
        public PagedResult<Nutrients> Data { get; set; }

        /// <summary>
        /// Contains search and filter criteria for the nutrients list.
        /// </summary>
        public NutrientsSearch Search { get; set; }
    }
}
