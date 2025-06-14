using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicos.Abstractions;
using Tecnicos.Data.Models;
using Tecnicos.Domain.DTOS;


namespace Api_clean_architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController(IComprasService comprasService) : ControllerBase
    {
        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComprasDto>>> GetCompras()
        {
            try
            {
                return await comprasService.Listar(p => true, HttpContext.RequestAborted);
            }
            catch (TaskCanceledException)
            {
                return StatusCode(499); // Solicitud cancelada
            }
            catch (Exception)
            {
                return StatusCode(500); // Error interno
            }
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComprasDto>> GetCompras(int id)
        {
            try
            {
                return await comprasService.Buscar(id, HttpContext.RequestAborted);
            }
            catch (TaskCanceledException)
            {
                return StatusCode(499); // Solicitud cancelada
            }
            catch (Exception)
            {
                return StatusCode(500); // Error interno
            }
        }

        // PUT: api/Compras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompras(int id, ComprasDto comprasDto)
        {
            try
            {
                if (id != comprasDto.CompraId)
                {
                    return BadRequest();
                }
                await comprasService.Guardar(comprasDto, HttpContext.RequestAborted);
                return NoContent();
            }
            catch (TaskCanceledException)
            {
                return StatusCode(499); // Solicitud cancelada
            }
            catch (Exception)
            {
                return StatusCode(500); // Error interno
            }
        }

        // POST: api/Compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compras>> PostCompras(ComprasDto compraDto)
        {
            try
            {
                await comprasService.Guardar(compraDto, HttpContext.RequestAborted);
                return CreatedAtAction("GetCompras", new { id = compraDto.CompraId }, compraDto);
            }
            catch (TaskCanceledException)
            {
                return StatusCode(499); // Solicitud cancelada
            }
            catch (Exception)
            {
                return StatusCode(500); // Error interno
            }
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompras(int id)
        {
            try
            {
                await comprasService.Eliminar(id, HttpContext.RequestAborted);
                return NoContent();
            }
            catch (TaskCanceledException)
            {
                return StatusCode(499); // Solicitud cancelada
            }
            catch (Exception)
            {
                return StatusCode(500); // Error interno
            }
        }
    }
}