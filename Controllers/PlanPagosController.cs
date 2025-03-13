using Microsoft.AspNetCore.Mvc;
using PrestamosService.Repositories;

namespace PrestamosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanPagosController : Controller
    {
        private readonly IPlanPagosRepository _planPagosRepository;
        private readonly IPrestamoRepository _prestamoRepository;

        public PlanPagosController(IPrestamoRepository prestamoRepository, IPlanPagosRepository planPagosRepository)
        {
            _planPagosRepository = planPagosRepository;
            _prestamoRepository = prestamoRepository;
        }

        [HttpGet("{prestamoId}")]
        public async Task<ActionResult> ObtenerPlanDePago(int prestamoId)
        {
            try
            {
                var planPagos = await _planPagosRepository.ObtenerPorPrestamoIdAsync(prestamoId);
                if (planPagos == null || !planPagos.Any())
                {
                    return NotFound($"No hay un plan de pagos asociado al préstamo con ID {prestamoId}.");
                }

                return Ok(planPagos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el plan de pagos: {ex.Message}");
            }
        }

        [HttpPost("{prestamoId}/generar")]
        public async Task<ActionResult> GenerarPlanDePagos(int prestamoId)
        {
            try
            {
                var prestamo = await _prestamoRepository.GetPrestamoAsync(prestamoId);
                if (prestamo == null)
                {
                    return NotFound($"Préstamo con ID {prestamoId} no encontrado.");
                }

                var planPagos = await _planPagosRepository.GenerarPlanDePagoAsync(prestamo);
                return Ok(planPagos); // ✅ Devolvemos el plan generado o existente
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar el plan de pagos: {ex.Message}");
            }
        }


        [HttpDelete("{prestamoId}/eliminar")]
        public async Task<IActionResult> EliminarPlanDePago(int prestamoId)
        {
            try
            {
                var prestamo = await _prestamoRepository.GetPrestamoAsync(prestamoId);
                if (prestamo == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el préstamo con ID {prestamoId}." });
                }

                await _planPagosRepository.EliminarPlanDePagoAsync(prestamoId);
                return Ok(new { mensaje = $"Plan de pagos para el préstamo {prestamoId} eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al eliminar el plan de pagos: {ex.Message}" });
            }
        }



    }
}
