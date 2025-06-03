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

        /// <summary>
        /// Obter todos os pedidos
        /// </summary>
        /// <returns>Todos os Pedidos de Ajuda </returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _repository.GetAllAsync();
            return Ok(pedidos);
        }

        /// <summary>
        /// Obtém um pedido pelo ID.
        /// </summary>
        /// <param name="id">Identificador do Pedido</param>
        /// <returns>Dados do Pedido</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _repository.GetByIdAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        // Create
        /// <summary>
        /// Cadastrar um Pedido de Ajuda
        /// </summary>
        /// <remarks>
        /// objeto Json
        /// </remarks>
        /// <param name="pedido">Dados do Pedido</param>
        /// <returns>Pedido recém criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoAjuda pedido)
        {
            await _repository.AddAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = pedido.PedidoId }, pedido);
        }

        // Update
        /// <summary>
        /// Atualizar um Pedido
        /// </summary>
        /// <param name="id">Identificador do Pedido</param>
        /// <param name="pedido">Dados do Pedido</param>
        /// <returns>Não retorna informações</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PedidoAjuda pedido)
        {
            if (id != pedido.PedidoId) return BadRequest("IDs não coincidem.");
            await _repository.UpdateAsync(pedido);
            return NoContent();
        }

        // Delete
        /// <summary>
        /// Deletar um Pedido
        /// </summary>
        /// <param name="id">Identificador de Pedido</param>
        /// <returns>Não retorna informações</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
