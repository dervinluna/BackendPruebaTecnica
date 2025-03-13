using Microsoft.EntityFrameworkCore;
using PrestamosService.Data;
using PrestamosService.Models;

namespace PrestamosService.Repositories
{
    public class PlanPagosRepository : IPlanPagosRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanPagosRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        // obtener plan de pagos de un prestamos en especifico
        public async Task<IEnumerable<PlanPago>> ObtenerPorPrestamoIdAsync(int prestamoId)
        {
            return await _context.PlanesPago
                .Where(pp => pp.PrestamoId == prestamoId)
                .OrderBy(pp => pp.NumeroCuota)
                .ToListAsync();
        }

        // generar plan de pagos
        public async Task GenerarPlanDePagoAsync(Prestamo prestamo)
        {
            decimal montoTotal = prestamo.Monto * 1.519m; //aca aplicamos el interes
            decimal cuotaMensual = Math.Round(montoTotal / prestamo.Cuotas, 0);

            var planPagos = new List<PlanPago>();
            for (int i = 0; i < prestamo.Cuotas; i++)
            {
                planPagos.Add(new PlanPago
                    {
                    PrestamoId = prestamo.Id,
                    NumeroCuota = i,
                    FechaPago = DateTime.Now.AddMonths(i),
                    Monto = cuotaMensual,
                });
            }

            _context.PlanesPago.AddRange(planPagos);
            await _context.SaveChangesAsync();
        }

        // eliminar plan de pagos
        public async Task EliminarPlanDePagoAsync(int prestamoId)
        {
            var pagos = await _context.PlanesPago
                .Where(pp => pp.PrestamoId == prestamoId)
                .ToListAsync();

            _context.PlanesPago.RemoveRange(pagos);
            await _context.SaveChangesAsync();
        }


    }
}
