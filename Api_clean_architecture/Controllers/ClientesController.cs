using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnicos.Abstractions;
using Tecnicos.Data.Models;
using Tecnicos.Domain.DTOS;


namespace Api_clean_architecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController(IClientesService clientesService) : ControllerBase
    {
        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesDto>>> GetClientes()
        {
            try
            {
                return await clientesService.Listar(p => true, HttpContext.RequestAborted);
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

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesDto>> GetClientes(int id)
        {
            try
            {
                return await clientesService.Buscar(id, HttpContext.RequestAborted);
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

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, ClientesDto clientesDto)
        {
            try
            {
                if (id != clientesDto.CompraId)
                {
                    return BadRequest();
                }
                await clientesService.Guardar(clientesDto, HttpContext.RequestAborted);
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(ClientesDto clienteDto)
        {
            try
            {
                await clientesService.Guardar(clienteDto, HttpContext.RequestAborted);
                return CreatedAtAction("GetClientes", new { id = clienteDto.CompraId }, clienteDto);
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

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(int id)
        {
            try
            {
                await clientesService.Eliminar(id, HttpContext.RequestAborted);
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