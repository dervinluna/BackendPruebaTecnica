using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestamosService.Models
{
    public class PlanPago
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Prestamo")]
        public int PrestamoId { get; set; }
        public Prestamo Prestamo { get; set; }  

        public int NumeroCuota { get; set; }
        public DateTime FechaPago { get; set; } 
        public decimal Monto { get; set; }
    }
}
