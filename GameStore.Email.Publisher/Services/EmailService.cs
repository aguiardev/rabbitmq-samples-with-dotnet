using GameStore.Email.Publisher.AppSettings.Interface;
using GameStore.Email.Publisher.Models;
using GameStore.Email.Publisher.Services.Interface;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GameStore.Email.Publisher.Services
{
    public class EmailService : IEmailService
    {
        private readonly ConnectionFactory _factory;
        private readonly IMessageBrokerSettings _messageBrokerSettings;

        public EmailService(IMessageBrokerSettings messageBrokerSettings)
        {
            _messageBrokerSettings = messageBrokerSettings;
            _factory = new ConnectionFactory
            {
                HostName = _messageBrokerSettings.Host
            };
        }

        public void SendMessage(MessageModel message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: _messageBrokerSettings.Queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var stringfieldMessage = JsonConvert.SerializeObject(message);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: _messageBrokerSettings.Queue,
                        basicProperties: null,
                        body: bytesMessage
                    );
                }
            }
        }
    }
}