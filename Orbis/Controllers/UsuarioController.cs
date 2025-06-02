using Microsoft.AspNetCore.Mvc;
using Orbis.Domain.Entities;
using Orbis.Domain.Repositories;

namespace Orbis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _repository.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario usuario)
        {
            await _repository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
                return BadRequest("IDs não coincidem.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Atualize os campos da instância rastreada
            existing.Nome = usuario.Nome;
            existing.Sobrenome = usuario.Sobrenome;
            existing.Senha = usuario.Senha;
            existing.Cep = usuario.Cep;

            await _repository.UpdateAsync(existing); 
            return NoContent();
        }

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
