using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.PublicAPI.Api
{
    public class HealthData
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0, 300, ErrorMessage = "Weight must be between 0 and 300")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Blood pressure is required")]
        public float Blood_pressure { get; set; }

        [Required(ErrorMessage = "Blood sugar is required")]
        public float Blood_sugar { get; set; }
    }
}
