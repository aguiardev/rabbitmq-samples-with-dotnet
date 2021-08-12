using GameStore.Email.Consumer.AppSettings;
using GameStore.Email.Consumer.AppSettings.Interface;
using GameStore.Email.Consumer.Models;
using GameStore.Email.Consumer.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GameStore.Email.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var emailService = new EmailService(GetMessageBrokerSettings());

            emailService.OnConsuming += (sender, ea) =>
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}] Received message");
                Console.WriteLine($"subject: {ea.Subject}");
                Console.WriteLine($"To: {string.Join(';', ea.To)}");
                Console.WriteLine($"Body: {ea.Body}");
            };

            emailService.SendMessage();

            Console.ReadLine();
        }

        static IMessageBrokerSettings GetMessageBrokerSettings()
        {
            var basePath = Directory.GetCurrentDirectory();

            basePath += @"\GameStore.Email.Consumer";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var section = configuration.GetSection("messageBrokerSettings");

            var messageBrokerSettings = new MessageBrokerSettings();
            section.Bind(messageBrokerSettings);

            return messageBrokerSettings;
        }
    }
}
