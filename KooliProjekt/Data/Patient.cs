using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Data
{
    public class Patient
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string HealthData { get; set; }

        public string Nutrition { get; set; }
    }
}
