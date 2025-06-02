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

        // Get all
        /// <summary>
        /// Obter todos os Usuarios
        /// </summary>
        /// <returns>Todos os Usuários </returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _repository.GetAllAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">Identificador do Usuário</param>
        /// <returns>Dados do Usuario</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        // Create
        /// <summary>
        /// Cadastrar um Usuario 
        /// </summary>
        /// <remarks>
        /// objeto Json
        /// </remarks>
        /// <param name="usuario">Dados do Usuario</param>
        /// <returns>Usuário recém criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario usuario)
        {
            await _repository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.UsuarioId }, usuario);
        }


        // Update
        /// <summary>
        /// Atualizar um Usuário
        /// </summary>
        /// <param name="id">Identificador do Usuario</param>
        /// <param name="usuario">Dados do Usuário</param>
        /// <returns>Não retorna informações</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
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

        // Delete
        /// <summary>
        /// Deletar um Usuario
        /// </summary>
        /// <param name="id">Identificador de Usuario</param>
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
