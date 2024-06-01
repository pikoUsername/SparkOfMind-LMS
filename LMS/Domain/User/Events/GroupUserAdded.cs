using LMS.Domain.User.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.User.Events
{
    public class GroupUserAdded(GroupEntity group, UserEntity user) : BaseEvent
    {
        public GroupEntity Group { get; set; } = group; 
        public UserEntity User { get; set; } = user;
    }
}
