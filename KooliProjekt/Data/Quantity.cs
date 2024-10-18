using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Quantity
    {
        public int id { get; set; }

        [Required]
        public Nutrients Nutrients { get; set; }

        [Required]
        public string Amount { get; set; }
    }
}
    