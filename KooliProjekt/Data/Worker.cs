using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Worker
    {
        public int id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        public Patient Patient { get; set; }
        public IList<Patient> Patients { get; set; }
    }
}
