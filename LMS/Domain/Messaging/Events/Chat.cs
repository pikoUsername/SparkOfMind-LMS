using LMS.Domain.Messaging.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Messaging.Events
{
    public class ChatRead(ChatEntity chat) : BaseEvent
    {
        public ChatEntity Chat { get; set; } = chat;
    }

    public class ChatCreated(ChatEntity chat) : BaseEvent
    {
        public ChatEntity Chat { get; set; } = chat;
    }
}
