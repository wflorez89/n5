using WilmerFlorez.Common.Kafka;

namespace WilmerFlorez.Utilities.Interfaces.Kafka
{
    public interface IEventProducer
    {
        void Produce(BaseEvent eventPublish);
    }
}
