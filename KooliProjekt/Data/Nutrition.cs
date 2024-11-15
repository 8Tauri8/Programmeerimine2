using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Nutrition
    {
        public int id { get; set; }

        [Required]
        public DateTime Eating_time { get; set; }

        [Required]
        public float Nutrients { get; set; }

        [Required]
        public float Quantity { get; set; }
    }
}
