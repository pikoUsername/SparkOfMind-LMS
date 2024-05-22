using LMS.Domain.User.Enums;
using LMS.Domain.User.Events;
using LMS.Domain.User.Interfaces;

namespace LMS.Domain.User.Entities
{
    public class RoleEntity : BaseAuditableEntity, IRoleEntity
    {
        public List<PermissionEntity> Permissions { get; set; } = null!;
        public UserRoles Role { get; set; }

        public static RoleEntity Create(UserRoles roleName, params PermissionEntity[] permissions)
        {
            var role = new RoleEntity()
            {
                Permissions = permissions.ToList(),
                Role = roleName,
            }; 

            return role; 
        }

        // Repeating code! 
        public void AddPermission(PermissionEntity permission)
        {
            Permissions.Add(permission);

            AddDomainEvent(new PermissionAdded("ROLE", permission));
        }

        public void AddPermissionWithCode(string subjectName, string subjectId, params string[] actions)
        {
            foreach (var action in actions)
            {
                AddPermission(PermissionEntity.Create(subjectName, subjectId, action));
            }
        }
    }
}
