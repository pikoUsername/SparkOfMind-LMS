using LMS.Domain.Messaging.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Messaging.Events
{
    public class MessageCreated(MessageEntity message) : BaseEvent
    {
        public MessageEntity Message { get; set; } = message;
    }
}
