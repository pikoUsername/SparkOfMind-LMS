using LMS.Domain.User.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.User.Events
{
    public class UserCreated(UserEntity User) : BaseEvent
    {
        public UserEntity User { get; set; } = User;
    }
}
