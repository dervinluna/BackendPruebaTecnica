using System.ComponentModel.DataAnnotations;

namespace PrestamosService.Models
{
    public class Prestamo
    {

        [Key]
        public int Id { get; set; } 
        public string Dpi { get; set; }
        public string NombreCliente { get; set; }
        public decimal Monto { get; set; }
        public int Cuotas { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;


    }
}
