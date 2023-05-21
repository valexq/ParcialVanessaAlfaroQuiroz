using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ParcialVanessaAlfaro.DAL.Entities
{
    public class Ticket
    {
        [Required]
        [Key]
        [Display(Name = "Código de boleta")]
        public Guid Id { get; set; }
        [Display(Name = "Fecha de uso")]
        public DateTime? UseDate { get; set; }
        [Display(Name = "Uso")]
        public Boolean IsUsed { get; set; }
        [Display(Name = "Portería de entrada")]
        public string? EntranceGate { get; set; }

    }
}
