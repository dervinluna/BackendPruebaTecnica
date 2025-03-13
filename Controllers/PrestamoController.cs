using Microsoft.AspNetCore.Mvc;
using PrestamosService.Models;
using PrestamosService.Repositories;

namespace PrestamosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : Controller
    {
      private readonly IPrestamoRepository _repository;

        public PrestamoController(IPrestamoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            return Ok(await _repository.GetPrestamoAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
        {
            var prestamo = await _repository.GetPrestamoAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            return prestamo;
        }

        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo(Prestamo prestamo)
        {
            await _repository.CreatePrestamoAsync(prestamo);
            return CreatedAtAction(nameof(GetPrestamo), new { id = prestamo.Id }, prestamo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamo(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return BadRequest();
            }
            await _repository.UpdatePrestamoAsync(prestamo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            await _repository.DeletePrestamoAsync(id);
            return NoContent();
        }



    }
}
