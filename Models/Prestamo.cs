using System.ComponentModel.DataAnnotations;

namespace PrestamosService.Models
{
    public class Prestamo
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El DPI es obligatorio.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El DPI debe tener exactamente 13 dígitos.")]
        [RegularExpression("^[0-9]{13}$", ErrorMessage = "El DPI debe contener solo números.")]
        public string Dpi { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del cliente no debe superar los 100 caracteres.")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(100, 1000000, ErrorMessage = "El monto debe estar entre Q.100 y Q.1,000,000.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Las cuotas son obligatorias.")]
        [Range(1, 360, ErrorMessage = "Las cuotas deben estar entre 1 y 360 meses.")]
        public int Cuotas { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;


    }
}
