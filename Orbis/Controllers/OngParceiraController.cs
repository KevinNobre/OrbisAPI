using Microsoft.AspNetCore.Mvc;
using Orbis.Domain.Entities;
using Orbis.Domain.Repositories;

namespace Orbis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OngParceiraController : ControllerBase
    {
        private readonly IOngParceiraRepository _repository;

        public OngParceiraController(IOngParceiraRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Obter todas as ONGs Parceiras
        /// </summary>
        /// <returns>Todas as ONGs </returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OngParceira>>> Get()
        {
            var ongs = await _repository.GetAllAsync();
            return Ok(ongs);
        }


        /// <summary>
        /// Obtém uma ONG pelo ID.
        /// </summary>
        /// <param name="id">Identificador da ONG</param>
        /// <returns>Dados da ONG</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<OngParceira>> GetById(int id)
        {
            var ong = await _repository.GetByIdAsync(id);
            if (ong == null)
                return NotFound();

            return Ok(ong);
        }

        /// <summary>
        /// Cadastrar uma ONG Parceira 
        /// </summary>
        /// <remarks>
        /// objeto Json
        /// </remarks>
        /// <param name="ong">Dados da ONG</param>
        /// <returns>ONG recém cadastrada</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost]
        public async Task<ActionResult> Create(OngParceira ong)
        {
            await _repository.AddAsync(ong);
            return CreatedAtAction(nameof(GetById), new { id = ong.OngId }, ong);
        }

        /// <summary>
        /// Atualizar uma ONG Parceira
        /// </summary>
        /// <param name="id">Identificador da ONG</param>
        /// <param name="ong">Dados da ONG</param>
        /// <returns>Não retorna informações</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OngParceira ong)
        {
            if (id != ong.OngId)
                return BadRequest("IDs não coincidem.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _repository.UpdateAsync(ong);
            return NoContent();
        }

        // Delete
        /// <summary>
        /// Deletar uma ONG Parceira
        /// </summary>
        /// <param name="id">Identificador da ONG</param>
        /// <returns>Não retorna informações</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
