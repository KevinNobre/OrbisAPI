using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Orbis.Mensageria.Services
{
    public class RabbitConsumer
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "pedido_ajuda_urgencia";
        private readonly string _username = "admin";
        private readonly string _password = "admin";

        public async Task StartAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += HandleMessageAsync;

            await channel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine("[Consumer] Aguardando mensagens da fila 'pedido_ajuda_urgencia'...");

            await Task.Delay(-1);
        }

        private Task HandleMessageAsync(object sender, BasicDeliverEventArgs @event)
        {
            var body = @event.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);

            try
            {
                var pedido = JsonSerializer.Deserialize<PedidoAjudaMensagem>(json);
                if (pedido != null)
                {
                    Console.WriteLine("============================");
                    Console.WriteLine($"[Recebido] Tipo de Ajuda: {pedido.TipoAjuda}");
                    Console.WriteLine($"Descrição: {pedido.Descricao}");
                    Console.WriteLine("============================\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }

    // Modelo da mensagem
    public class PedidoAjudaMensagem
    {
        public string TipoAjuda { get; set; }
        public string Descricao { get; set; }
    }
}
