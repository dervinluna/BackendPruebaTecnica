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


        // Generar plan de pagos
        public async Task GenerarPlanDePagoAsync(Prestamo prestamo)
        {
            // Verificar si ya existe un plan de pagos para este préstamo
            if (await _context.PlanesPago.AnyAsync(pp => pp.PrestamoId == prestamo.Id))
            {
                throw new InvalidOperationException($"Ya existe un plan de pagos para el préstamo con ID {prestamo.Id}.");
            }

            decimal montoTotal = prestamo.Monto * 1.519m; // Aplicamos el interés
            decimal cuotaMensual = Math.Round(montoTotal / prestamo.Cuotas, 0);

            var planPagos = new List<PlanPago>();
            for (int i = 1; i <= prestamo.Cuotas; i++) // Cuotas deben empezar en 1
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



        // Eliminar plan de pagos
        public async Task EliminarPlanDePagoAsync(int prestamoId)
        {
            var pagos = await _context.PlanesPago
                .Where(pp => pp.PrestamoId == prestamoId)
                .ToListAsync();

            if (!pagos.Any())
            {
                throw new InvalidOperationException($"No existe un plan de pagos para el préstamo con ID {prestamoId}.");
            }

            _context.PlanesPago.RemoveRange(pagos);
            await _context.SaveChangesAsync();
        }


    }
}
