using LMS.Domain.User.Entities;

namespace LMS.Infrastructure.Caching
{
    public interface INotificationCache
    {
        Task<ICollection<NotificationEntity>> GetNewNotifications(Guid userId);
        Task AddNotification(Guid userId, NotificationEntity notification);
    }
}
