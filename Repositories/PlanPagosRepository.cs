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


        // Generar plan de pagos y devolverlo
        public async Task<IEnumerable<PlanPago>> GenerarPlanDePagoAsync(Prestamo prestamo)
        {
            // 1️⃣ Verificar si ya existe un plan de pagos
            var planPagosExistente = await ObtenerPorPrestamoIdAsync(prestamo.Id);
            if (planPagosExistente.Any())
            {
                return planPagosExistente; // ✅ Devolver el plan ya existente
            }

            // 2️⃣ Si no existe, generarlo
            decimal montoTotal = prestamo.Monto * 1.519m; // Aplicamos el interés
            decimal cuotaMensual = Math.Round(montoTotal / prestamo.Cuotas, 0);

            var planPagos = new List<PlanPago>();
            for (int i = 1; i <= prestamo.Cuotas; i++)
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

            return planPagos; // ✅ Devolver el nuevo plan generado
        }



        // Eliminar plan de pagos
        public async Task EliminarPlanDePagoAsync(int prestamoId)
        {
            var pagos = await _context.PlanesPago
                .Where(pp => pp.PrestamoId == prestamoId)
                .ToListAsync();

            if (!pagos.Any())
            {
                return; // ❌ Si no hay planes de pago, simplemente salir sin error
            }

            _context.PlanesPago.RemoveRange(pagos);
            await _context.SaveChangesAsync();
        }



    }
}
