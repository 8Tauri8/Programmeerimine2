using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Data
{
    public class Nutrition
    {
        public int NutritionID { get; set; }

        [Required]
        public DateTime Eating_time { get; set; }

        [Required]
        public Nutrients Nutrients { get; set; }

        [Required]
        public Quantity Quantity { get; set; }
    }
}
