using LMS.Domain.User.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.User.Events
{
    public class PermissionAdded(string type, PermissionEntity permission) : BaseEvent
    {
        public string Type { get; set; } = type; 
        public PermissionEntity Permission { get; set; } = permission;
    }

    public class PermissionCreated(PermissionEntity permission) : BaseEvent
    {
        public PermissionEntity Permission { get; set; } = permission; 
    }
}
