using System;
using System.Threading.Tasks;
using Orbis.Mensageria.Services;

namespace Orbis.Mensageria
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Iniciando o consumidor RabbitMQ...");

            var consumer = new RabbitConsumer();
            await consumer.StartAsync();

            Console.WriteLine("Pressione [Enter] para sair.");
            Console.ReadLine();
        }
    }
}
