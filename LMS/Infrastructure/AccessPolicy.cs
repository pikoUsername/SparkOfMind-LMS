using LMS.Application.Common.Interfaces;
using LMS.Domain.User.Entities;
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

        public Task<bool> IsAllowed(IAccessUser actor, string action, object relation)
        {
            foreach (var permissionAcl in actor.GetPermissions())
            {
                if (CheckPermission(permissionAcl.Join(), action, relation))
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> Role(IAccessUser actor, string roleName)
        {
            foreach (var userRole in actor.GetRoles())
            {
                if (userRole.Name == roleName)
                {
                    return Task.FromResult(true); 
                }
            }
            return Task.FromResult(false);
        }

        public async Task<bool> Relationship(IAccessUser actor, string action, object relation, Guid ownerId)
        {
            var result = await IsAllowed(actor, action, relation) || ownerId == actor.Id; 
            if (!result)
            {
                throw new AccessDenied("relationship check failed"); 
            }
            return result; 
        }

        public async Task<UserEntity> GetCurrentUser()
        {
            if (_cachedCurrentUser == null)
            {
                // Retrieve current user from the database
                _cachedCurrentUser = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == _currentUser.Id);

                // If current user is not found or blocked, throw AccessDenied exception
                if (_cachedCurrentUser == null || _cachedCurrentUser.Blocked)
                {
                    throw new AccessDenied("User is not authorized");
                }
            }
            return _cachedCurrentUser;
        }

        public async Task EnforceIsAllowed(IAccessUser actor, string action, object relation)
        {
            var allowed = await IsAllowed(actor, action, relation);
            if (!allowed)
            {
                throw new AccessDenied($"User is not allowed to perform action '{action}' on relation '{relation}'");
            }
        }

        public async Task EnforceRole(IAccessUser actor, string role)
        {
            var hasRole = await Role(actor, role);
            if (!hasRole)
            {
                throw new AccessDenied($"User does not have the required role '{role}'");
            }
        }

        public async Task EnforceRelationship(IAccessUser actor, string action, object relation, Guid ownerId)
        {
            var allowed = await Relationship(actor, action, relation, ownerId);
            if (!allowed)
            {
                throw new AccessDenied($"User is not allowed to perform action '{action}' on relation '{relation}' with ownerId '{ownerId}'");
            }
        }

        private bool CheckPermission(string permissionAcl, string action, object relation)
        {
            var parts = permissionAcl.Split(':');
            if (parts.Length != 3)
            {
                throw new Exception(
                    $"Pemissions acl of the user has been compromised, parts: {parts}, acl: {permissionAcl}");
            }
            var subjectName = parts[0];
            var subjectId = parts[1];
            var subjectAction = parts[2];

            return (subjectId == relation.ToString() || subjectId == "*") && subjectName == relation.GetType().Name && subjectAction == action; 
        }

    }
}
