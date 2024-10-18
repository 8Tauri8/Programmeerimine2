using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class HealthData
    {
        public int id { get; set; }

        [Required]
        public float Weight { get; set; }

        [Required]
        public float Blood_pressure { get; set; }

        [Required]
        public float Blood_sugar { get; set; }
    }
}
