using GameStore.Email.Publisher.Models;

namespace GameStore.Email.Publisher.Services.Interface
{
    public interface IEmailService
    {
        void SendMessage(MessageModel message);
    }
}