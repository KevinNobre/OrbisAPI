using Microsoft.AspNetCore.Mvc;
using Orbis.Domain.Entities;
using Orbis.Domain.Repositories;

namespace Orbis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoAjudaController : ControllerBase
    {
        private readonly IPedidoAjudaRepository _repository;

        public PedidoAjudaController(IPedidoAjudaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _repository.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _repository.GetByIdAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoAjuda pedido)
        {
            await _repository.AddAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = pedido.PedidoId }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PedidoAjuda pedido)
        {
            if (id != pedido.PedidoId) return BadRequest("IDs não coincidem.");
            await _repository.UpdateAsync(pedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
