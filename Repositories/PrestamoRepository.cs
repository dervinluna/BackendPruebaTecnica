using Microsoft.EntityFrameworkCore;
using PrestamosService.Data;
using PrestamosService.Models;

namespace PrestamosService.Repositories
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly ApplicationDbContext _context;

        public PrestamoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prestamo>> GetPrestamoAsync()
        {
            try
            {
                return await _context.Prestamos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener préstamos: {ex.Message}");
            }
        }

        public async Task<Prestamo> GetPrestamoAsync(int id)
        {
            try
            {
                return await _context.Prestamos.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el préstamo con ID {id}: {ex.Message}");
            }
        }

        public async Task CreatePrestamoAsync(Prestamo prestamo)
        {
            try
            {
                if (prestamo == null || prestamo.Monto <= 0 || prestamo.Cuotas <= 0)
                {
                    throw new ArgumentException("Datos inválidos. El monto y las cuotas deben ser mayores a 0.");
                }

                _context.Prestamos.Add(prestamo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el préstamo: {ex.Message}");
            }
        }

        public async Task UpdatePrestamoAsync(Prestamo prestamo)
        {
            var existingPrestamo = await _context.Prestamos.FindAsync(prestamo.Id);

            if (existingPrestamo != null)
            {
                _context.Entry(existingPrestamo).CurrentValues.SetValues(prestamo);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"No se encontró un préstamo con ID {prestamo.Id}.");
            }
        }


        public async Task DeletePrestamoAsync(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return; // Simplemente retorna si no existe, sin lanzar excepción.
            }

            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
        }

    }
}
