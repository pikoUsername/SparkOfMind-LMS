using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using LMS.Domain.User.Events;

namespace LMS.Domain.User.ValueObjects
{
    public class PermissionCollection : List<PermissionEntity>
    {
        public void AddPermission(PermissionEntity permission)
        {
            Add(permission);

            permission.AddDomainEvent(new PermissionAdded("GROUP", permission));
        }

        public void AddPermissionWithCode(BaseEntity subject, params PermissionEnum[] actions)
        {
            foreach (var action in actions)
            {
                AddPermission(PermissionEntity.Create(subject.GetType().Name, subject.Id, action));
            }
        }
    }
}
