using Microsoft.EntityFrameworkCore;    
using PrestamosService.Models;

namespace PrestamosService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<PlanPago> PlanesPago { get; set; }
    }
}
