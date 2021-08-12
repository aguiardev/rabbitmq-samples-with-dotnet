namespace GameStore.Email.Consumer.AppSettings.Interface
{
    public interface IMessageBrokerSettings
    {
        string Host { get; set; }
        string Queue { get; set; }
    }
}