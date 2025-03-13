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
            try
            {
                if (!await _context.Prestamos.AnyAsync(p => p.Id == prestamo.Id))
                {
                    throw new KeyNotFoundException($"No se encontró el préstamo con ID {prestamo.Id}.");
                }

                _context.Entry(prestamo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el préstamo: {ex.Message}");
            }
        }

        public async Task DeletePrestamoAsync(int id)
        {
            try
            {
                var prestamo = await _context.Prestamos.FindAsync(id);
                if (prestamo == null)
                {
                    throw new KeyNotFoundException($"No se encontró el préstamo con ID {id}.");
                }

                _context.Prestamos.Remove(prestamo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el préstamo: {ex.Message}");
            }
        }
    }
}
