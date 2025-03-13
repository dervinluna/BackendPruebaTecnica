using Microsoft.AspNetCore.Mvc;
using PrestamosService.Models;
using PrestamosService.Repositories;

namespace PrestamosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : Controller
    {
        private readonly IPrestamoRepository _repository;

        public PrestamoController(IPrestamoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            try
            {
                var prestamos = await _repository.GetPrestamoAsync();
                return Ok(prestamos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener préstamos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
        {
            try
            {
                var prestamo = await _repository.GetPrestamoAsync(id);
                if (prestamo == null)
                {
                    return NotFound($"No se encontró el préstamo con ID {id}.");
                }
                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener préstamo: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo(Prestamo prestamo)
        {
            try
            {
                if (prestamo == null || prestamo.Monto <= 0 || prestamo.Cuotas <= 0)
                {
                    return BadRequest("Datos inválidos. El monto y las cuotas deben ser mayores a 0.");
                }

                await _repository.CreatePrestamoAsync(prestamo);
                return CreatedAtAction(nameof(GetPrestamo), new { id = prestamo.Id }, prestamo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear préstamo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamo(int id, Prestamo prestamo)
        {
            try
            {
                if (id != prestamo.Id)
                {
                    return BadRequest("El ID en la URL no coincide con el ID del préstamo.");
                }

                var existePrestamo = await _repository.GetPrestamoAsync(id);
                if (existePrestamo == null)
                {
                    return NotFound($"No se encontró el préstamo con ID {id}.");
                }

                await _repository.UpdatePrestamoAsync(prestamo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar préstamo: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            try
            {
                var prestamo = await _repository.GetPrestamoAsync(id);
                if (prestamo == null)
                {
                    return NotFound($"No se encontró el préstamo con ID {id}.");
                }

                await _repository.DeletePrestamoAsync(id);
                return Ok($"Préstamo con ID {id} eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar préstamo: {ex.Message}");
            }
        }



    }
}
