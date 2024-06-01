using LMS.Domain.User.Entities;

namespace LMS.Domain.User.Events
{
    public class UserBlocked(UserEntity user) : DomainEvent
    {
        public UserEntity User { get; set; } = user; 
    }
}
