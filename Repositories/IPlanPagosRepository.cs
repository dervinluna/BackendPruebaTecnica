using PrestamosService.Models;

namespace PrestamosService.Repositories
{
    public interface IPlanPagosRepository
    {
        Task<IEnumerable<PlanPago>> ObtenerPorPrestamoIdAsync(int prestamoId);
        Task<IEnumerable<PlanPago>> GenerarPlanDePagoAsync(Prestamo prestamo);
        Task EliminarPlanDePagoAsync(int prestamoId);

    }
}
