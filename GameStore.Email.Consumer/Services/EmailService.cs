using GameStore.Email.Consumer.AppSettings.Interface;
using GameStore.Email.Consumer.Events;
using GameStore.Email.Consumer.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace GameStore.Email.Consumer.Services
{
    public delegate void OnConsumingHandler(object sender, EventingConsumerArgs e);

    public class EmailService
    {
        private readonly ConnectionFactory _factory;
        private readonly IMessageBrokerSettings _messageBrokerSettings;
        public event OnConsumingHandler OnConsuming;

        public EmailService(IMessageBrokerSettings messageBrokerSettings)
        {
            _messageBrokerSettings = messageBrokerSettings;
            _factory = new ConnectionFactory
            {
                HostName = _messageBrokerSettings.Host
            };
        }

        public void SendMessage()
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

                    var consumer = new EventingBasicConsumer(channel);
                    
                    consumer.Received += (model, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            var messagem = JsonSerializer.Deserialize<MessageModel>(message);
                            
                            OnConsuming.Invoke(this, new EventingConsumerArgs(
                                messagem.Subject, messagem.To, messagem.Body
                            ));

                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                        catch (Exception ex)
                        {
                            channel.BasicNack(ea.DeliveryTag, false, true);
                        }
                    };

                    channel.BasicConsume(
                        queue: _messageBrokerSettings.Queue,
                        autoAck: false, //
                        consumer: consumer);
                }
            }
        }
    }
}