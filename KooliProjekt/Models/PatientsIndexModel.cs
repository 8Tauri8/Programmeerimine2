using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class PatientsIndexModel
    {
        public HealthDatasSearch Search { get; set; }
        public PagedResult<Patient> Data { get; set; }
    }
}
