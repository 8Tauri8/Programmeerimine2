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
        public HealthData HealthData { get; set; }

        [Required]
        public Nutrition Nutrition { get; set; }
    }
}
