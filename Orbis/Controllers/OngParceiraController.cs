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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OngParceira>>> Get()
        {
            var ongs = await _repository.GetAllAsync();
            return Ok(ongs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OngParceira>> GetById(int id)
        {
            var ong = await _repository.GetByIdAsync(id);
            if (ong == null)
                return NotFound();

            return Ok(ong);
        }

        [HttpPost]
        public async Task<ActionResult> Create(OngParceira ong)
        {
            await _repository.AddAsync(ong);
            return CreatedAtAction(nameof(GetById), new { id = ong.OngId }, ong);
        }

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
