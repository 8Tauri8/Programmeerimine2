using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Data
{
    public class Patient
    {
        public int id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        public string HealthData { get; set; }

        [Required]
        public string Nutrition { get; set; }
    }
}
