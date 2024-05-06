﻿using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;

namespace LMS.Domain.User.Interfaces
{
    public interface IRoleEntity
    {
        string Name { get; set; }
        ICollection<PermissionEntity> Permissions { get; set; }
        UserRoles? Role { get; set; }
    }
}
