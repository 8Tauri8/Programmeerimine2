using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.PublicAPI.Api
{
    public class HealthData
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Blood pressure is required")]
        public float Blood_pressure { get; set; }

        [Required(ErrorMessage = "Blood sugar is required")]
        public float Blood_sugar { get; set; }
    }
}
