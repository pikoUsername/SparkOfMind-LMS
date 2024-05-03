using LMS.Domain.User.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.User.Events
{
    public class UserDeleted(UserEntity user, string reason) : BaseEvent
    {
        public UserEntity User { get; set; } = user;
        public string Reason { get; set; } = reason;
    }
}
