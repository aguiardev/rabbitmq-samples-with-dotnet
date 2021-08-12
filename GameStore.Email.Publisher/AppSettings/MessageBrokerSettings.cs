using GameStore.Email.Publisher.AppSettings.Interface;

namespace GameStore.Email.Publisher.AppSettings
{
    public class MessageBrokerSettings : IMessageBrokerSettings
    {
        public string Host { get; set; }
        public string Queue { get; set; }
    }
}