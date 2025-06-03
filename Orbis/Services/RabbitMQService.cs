using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Orbis.Services
{
    public class RabbitMQService
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "pedido_ajuda_urgencia";
        private readonly string _username = "admin";
        private readonly string _password = "admin";

        public async Task PublicarMensagemAsync(string tipoAjuda, string descricao)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            try
            {
                await using var connection = await factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();

                // Declara fila durável para não perder mensagens se o RabbitMQ reiniciar
                await channel.QueueDeclareAsync(
                    queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var mensagem = new PedidoAjudaMensagem
                {
                    TipoAjuda = tipoAjuda,
                    Descricao = descricao
                };

                var json = JsonSerializer.Serialize(mensagem);
                var body = Encoding.UTF8.GetBytes(json);

                var properties = new BasicProperties
                {
                    Persistent = true // mensagem persistente
                };

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: _queueName,
                    mandatory: false,
                    basicProperties: properties,
                    body: body
                );
            }
            catch (Exception ex)
            {
                // Você pode tratar log, relançar ou lidar com erro aqui conforme sua necessidade
                Console.WriteLine($"Erro ao publicar mensagem: {ex.Message}");
                throw;
            }
        }
    }
    public class PedidoAjudaMensagem
    {
        public string TipoAjuda { get; set; }
        public string Descricao { get; set; }
    }
}
