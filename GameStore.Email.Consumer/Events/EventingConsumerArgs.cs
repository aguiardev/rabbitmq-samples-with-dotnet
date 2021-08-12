using System;

namespace GameStore.Email.Consumer.Events
{
    public class EventingConsumerArgs : EventArgs
    {
        public string Subject { get; set; }
        public string[] To { get; set; }
        public string Body { get; set; }

        public EventingConsumerArgs()
        {
            
        }

        public EventingConsumerArgs(string subject, string[] to, string body)
        {
            Subject = subject;
            To = to;
            Body = body;
        }
    }
}