using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;

namespace LMS.Domain.User.Interfaces
{
    public interface IRoleEntity
    {
        UserRoles Role { get; set; }
    }
}
