using Confluent.Kafka;
namespace WebApi.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ProducerConfig _config;
        private readonly IProducer<Null, string> _producer;
        public ProducerService()
        {
            _config = new ProducerConfig { BootstrapServers = "kafka:9092" };
            _producer = new ProducerBuilder<Null, string>(_config).Build();
        }
        public async Task Produce(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}
