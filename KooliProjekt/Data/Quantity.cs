using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Quantity
    {
        public int QuantityID { get; set; }

        [Required]
        public Nutrients Nutrients { get; set; }

        [Required]
        public string Amount { get; set; }
    }
}
    