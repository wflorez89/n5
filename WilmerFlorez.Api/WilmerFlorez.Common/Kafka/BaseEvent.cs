using System;

namespace WilmerFlorez.Common.Kafka
{
    public  class BaseEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NameOperation { get; set; }
    }
}
