namespace GameStore.Email.Consumer.Models
{
    public class MessageModel
    {
        public string Subject { get; set; }
        public string[] To { get; set; }
        public string Body { get; set; }
    }
}