﻿using LMS.Application.Common.Interfaces;
using LMS.Application.User.Exceptions;
using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using LMS.Domain.User.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure
{
    public class AccessPolicy : IAccessPolicy
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _currentUser;
        private UserEntity? _cachedCurrentUser;

        public AccessPolicy(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _currentUser = user;
        }

        public async Task<bool> IsAllowed(IAccessUser actor, string action, object relation)
        {
            foreach (var permissionAcl in actor.GetPermissions())
            {
                if (CheckPermission(permissionAcl.Join(), relation, action))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Role(IAccessUser actor, string roleName)
        {
            foreach (var userRole in actor.GetRoles())
            {
                if (userRole.Name == roleName)
                {
                    return await IsAllowed(actor, userRole.Permissions());
                }
            }
            return false;
        }

        public async Task<bool> CanAccessResource(IAccessUser actor, string action, object relation)
        {
            return await IsAllowed(actor, action, relation);
        }

        public async Task<UserEntity> GetCurrentUser()
        {
            if (_cachedCurrentUser == null)
            {
                // Retrieve current user from the database
                _cachedCurrentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUser.Id);

                // If current user is not found or blocked, throw AccessDenied exception
                if (_cachedCurrentUser == null || _cachedCurrentUser.Blocked)
                {
                    throw new AccessDenied("User is not authorized");
                }
            }
            return _cachedCurrentUser;
        }

        private bool CheckPermission(string permissionAcl, object relation, string action)
        {
            var parts = permissionAcl.Split(':');
            var subjectName = parts[0];
            var subjectId = parts[1];
            var subjectAction = parts[2];

            return (subjectId == relation.ToString() || subjectId == "*") && subjectName == relation.GetType().Name && subjectAction == action; 
        }
    }
}
