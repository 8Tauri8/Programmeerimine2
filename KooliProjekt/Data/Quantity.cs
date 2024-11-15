using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Quantity
    {
        public int id { get; set; }

        [Required]
        public float Nutrients { get; set; }

        [Required]
        public float Amount { get; set; }
    }
}
    