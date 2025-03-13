using PrestamosService.Data;
using PrestamosService.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Prestamos.ToListAsync();
        }

        public async Task<Prestamo> GetPrestamoAsync(int id)
        {
            return await _context.Prestamos.FindAsync(id);
        }

        public async Task CreatePrestamoAsync(Prestamo prestamo)
        {
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePrestamoAsync(Prestamo prestamo)
        {
            _context.Entry(prestamo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePrestamoAsync(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null )
            {
                _context.Prestamos.Remove(prestamo);
                await _context.SaveChangesAsync();

            }
           
        }
    }
}
