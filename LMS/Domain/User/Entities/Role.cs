using LMS.Domain.User.Enums;

namespace LMS.Domain.User.Entities
{
    public class RoleEntity : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public PermissionEntity Permission { get; set; } = null!;
        public UserRoles Role { get; set; }
    }
}
