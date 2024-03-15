public interface IProducerService
{
    Task Produce(string topic, string message);
}