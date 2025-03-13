using PrestamosService.Models;

namespace PrestamosService.Repositories
{
    public interface IPrestamoRepository
    {
        Task<IEnumerable<Prestamo>> GetPrestamoAsync();
        Task<Prestamo> GetPrestamoAsync(int id);
        Task CreatePrestamoAsync(Prestamo prestamo);
        Task UpdatePrestamoAsync(Prestamo prestamo);
        Task DeletePrestamoAsync(int id);
    }
}
