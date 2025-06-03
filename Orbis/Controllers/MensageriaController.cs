using Microsoft.AspNetCore.Mvc;
using Orbis.Services;

namespace Orbis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensageriaController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQService;

        public MensageriaController(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        // POST api/mensageria/enviar
        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarMensagem([FromBody] PedidoAjudaMensagem mensagem)
        {
            if (mensagem == null || string.IsNullOrWhiteSpace(mensagem.TipoAjuda) || string.IsNullOrWhiteSpace(mensagem.Descricao))
                return BadRequest("TipoAjuda e Descricao são obrigatórios.");

            await _rabbitMQService.PublicarMensagemAsync(mensagem.TipoAjuda, mensagem.Descricao);

            return Ok(new { mensagem = "Mensagem enviada com sucesso!" });
        }
    }

    // Modelo da mensagem, se preferir, pode ficar em outro arquivo
    public class PedidoAjudaMensagem
    {
        public string TipoAjuda { get; set; }
        public string Descricao { get; set; }
    }
}