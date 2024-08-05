using App.Kafka.Consumer.Console.ConfigKafka;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App.Kafka.Consumer.Console.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ConsumerConfig _consumerConfig;

        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;
            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = ParametersConfig.BOOTSTRAP_SERVER,
                // Definindo um grupo de consumer para ser usado quando for inscrever na fila do Kafka
                GroupId = ParametersConfig.GROUP_ID,
                // Define que a mensagem quando for consumida foi lida
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            // Inicializando a classe consumer com as configurações passada acima
            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Iniciando serviço de recebimento da mensagem");

            // Método responsável por se inscrever na fila do Kafka para observar a fila passada no parametro e consumir a mensagem quando tiver
            _consumer.Subscribe(ParametersConfig.TOPIC_NAME);

            while (!stoppingToken.IsCancellationRequested) 
            { 
                await Task.Run(() =>
                {

                    var result = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"GroupId: {ParametersConfig.GROUP_ID} Mensagem: {result.Message.Value}");
                });
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Unsubscribe();
            _logger.LogInformation($"Aplicação parou, conexão fechada");
            return Task.CompletedTask;
        }
    }
}
