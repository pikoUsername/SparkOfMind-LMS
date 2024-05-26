using LMS.Domain.User.Entities;

namespace LMS.Domain.User.Events
{
    public class GroupCreated(GroupEntity group) : DomainEvent 
    {
        public GroupEntity Group { get; set; } = group; 
    }
}
