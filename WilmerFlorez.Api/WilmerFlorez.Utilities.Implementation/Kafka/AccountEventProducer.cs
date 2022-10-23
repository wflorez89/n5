using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WilmerFlorez.Common.Kafka;
using WilmerFlorez.Utilities.Interfaces.Kafka;

namespace WilmerFlorez.Utilities.Implementation.Kafka
{
    public class AccountEventProducer : IEventProducer
    {
        public KafkaSettings _kafkaSettings;

        public AccountEventProducer(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
        }

        public void Produce(BaseEvent @event)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaSettings.Hostname
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                string value = JsonConvert.SerializeObject(@event);
                var message = new Message<Null, string> { Value = value };
                producer.ProduceAsync(_kafkaSettings.Topic, message)
                     .GetAwaiter().GetResult();
            }
        }
    }
}
