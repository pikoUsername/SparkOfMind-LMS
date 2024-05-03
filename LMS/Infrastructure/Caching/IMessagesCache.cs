using LMS.Domain.Messaging.Entities;

namespace LMS.Infrastructure.Caching
{
    public interface IMessagesCache
    {
        Task<ICollection<MessageEntity>> GetNewMessages(Guid userId);
        Task AddMessage(Guid userId, MessageEntity message);
    }
}
