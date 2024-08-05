using Confluent.Kafka;

namespace WebApi.Kafka.Producer.Api.Services
{
    public class ProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProducerService> _logger;
        private readonly ProducerConfig _producerConfig;

        public ProducerService(IConfiguration configuration, ILogger<ProducerService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            var bootstrap = _configuration.GetSection("KafkaConfig").GetSection("BootstrapServer").Value;

            // Configuração do servidor do Kafka
            _producerConfig = new ProducerConfig()
            {
                BootstrapServers = bootstrap
            };
        }

        public async Task<string> SendMessage(string message)
        {
            _logger.LogInformation("Iniciando o serviço de envio de mensagem");

            var topic = _configuration.GetSection("KafkaConfig").GetSection("TopicName").Value;

            try
            {
                using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                    var result = await producer.ProduceAsync(topic: topic, message: new() { Value = message });

                    var msgEnviada = $"{result.Status.ToString()} - {message}";
                    _logger.LogInformation(msgEnviada);

                    _logger.LogInformation($"Finalizado o serviço de envio de mensagem");
                    return msgEnviada;
                }
            }
            catch
            {
                _logger.LogError("Algo deu errado no serviço de envio de mensagem");
                return "Algo deu errado no serviço de envio de mensagem";
            }
        }
    } 
}
