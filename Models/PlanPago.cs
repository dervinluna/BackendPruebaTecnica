using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestamosService.Models
{
    public class PlanPago
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del préstamo es obligatorio.")]
        [ForeignKey("Prestamo")]
        public int PrestamoId { get; set; }
        public Prestamo Prestamo { get; set; }

        [Required(ErrorMessage = "El número de cuota es obligatorio.")]
        [Range(1, 360, ErrorMessage = "El número de cuota debe estar entre 1 y 360.")]
        public int NumeroCuota { get; set; }

        [Required(ErrorMessage = "La fecha de pago es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }

        [Required(ErrorMessage = "El monto de la cuota es obligatorio.")]
        [Range(1, 100000, ErrorMessage = "El monto debe estar entre Q.1 y Q.100,000.")]
        public decimal Monto { get; set; }
    }
}
