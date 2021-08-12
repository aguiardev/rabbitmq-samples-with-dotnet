using GameStore.Email.Consumer.AppSettings.Interface;

namespace GameStore.Email.Consumer.AppSettings
{
    public class MessageBrokerSettings : IMessageBrokerSettings
    {
        public string Host { get; set; }
        public string Queue { get; set; }
    }
}