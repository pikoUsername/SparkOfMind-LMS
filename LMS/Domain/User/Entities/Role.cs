using LMS.Domain.User.Enums;
using LMS.Domain.User.Interfaces;

namespace LMS.Domain.User.Entities
{
    public class RoleEntity : BaseAuditableEntity, IRoleEntity
    {
        public UserRoles Role { get; set; }

        public static RoleEntity Create(UserRoles roleName)
        {
            var role = new RoleEntity()
            {
                Role = roleName,
            }; 

            return role; 
        }
    }
}
