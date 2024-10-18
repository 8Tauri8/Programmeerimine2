using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Data
{
    public class Nutrients
    {

        public int NutrientsID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Sugar { get; set; }

        [Required]
        public float Fat { get; set; }

        [Required]
        public float Carbohydrates { get; set; }
    }
}
