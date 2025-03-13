using Microsoft.AspNetCore.Mvc;
using PrestamosService.Data;
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
            var planPagos = await _planPagosRepository.ObtenerPorPrestamoIdAsync(prestamoId);
            if (planPagos == null || !planPagos.Any())
            {
                return NotFound("No hay un plan de pagos asociado a este préstamo");
            }

            return Ok(planPagos);
        }

        [HttpPost("{prestamoId}/generar")]
        public async Task<ActionResult> GenerarPlanDePagos(int prestamoId)
        {
            var prestamo = await _prestamoRepository.GetPrestamoAsync(prestamoId);
            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }
            await _planPagosRepository.GenerarPlanDePagoAsync(prestamo);
            return Ok("Plan de pagos generado exitosamente");
        }

        [HttpDelete("{prestamoId}/eliminar")]
        public async Task<ActionResult> EliminarPlanDePago(int prestamoId)
        {
            var prestamo = await _prestamoRepository.GetPrestamoAsync(prestamoId);
            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }
            await _planPagosRepository.EliminarPlanDePagoAsync(prestamoId);
            return Ok("Plan de pagos eliminado exitosamente");
        }


    }
}
