using LMS.Application.Common.Interfaces;
using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using LMS.Domain.User.Interfaces;
using LMS.Domain.User.Services;
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

        public async Task<bool> IsAllowed(PermissionEnum action, object relation, IAccessUser? actor = null)
        {
            if (actor == null)
                actor = await GetCurrentUser();
            if (actor.IsSuperadmin)
                return true;
            foreach (var permissionAcl in actor.GetPermissions())
            {
                if (PermissionService.CheckPermissions(permissionAcl.Join(), action, relation))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Role(UserRoles role, IAccessUser? actor = null)
        {
            if (actor == null)
                actor = (IAccessUser)await GetCurrentUser();
            if (actor.IsSuperadmin)
                return true; 
            foreach (var userRole in actor.GetRoles())
            {
                if (userRole.Role == role)
                {
                    return true; 
                }
            }
            return false;
        }

        public async Task<bool> Relationship(PermissionEnum action, object relation, Guid ownerId, IAccessUser? actor = null)
        {
            var result = await IsAllowed(action, relation, actor) || ownerId == actor.Id; 
            if (!result)
            {
                throw new AccessDenied("relationship check failed"); 
            }
            return result; 
        }

        public async Task<UserEntity> GetCurrentUser()
        {
            if (_currentUser.Id == null)
                throw new AccessDenied("Unauthorized"); 
            if (_cachedCurrentUser == null)
            {
                _cachedCurrentUser = await GetUserById((Guid)_currentUser.Id); 

                if (_cachedCurrentUser == null || _cachedCurrentUser.Blocked)
                {
                    throw new AccessDenied("User is not authorized");
                }
            }
            return _cachedCurrentUser;
        }

        private async Task<UserEntity> GetUserById(Guid userId)
        {
            // performance eater around 150ms from cold start when permissions is around 0!!! 
            var user = await _context.Users
                .Include(x => x.Roles)
                .Include(x => x.Groups)
                    .ThenInclude(x => x.Permissions)
                .Include(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == userId);

            Guard.Against.NotFound(userId, user);

            return user; 
        }

        public async Task EnforceIsAllowed(PermissionEnum action, object relation, Guid? actorId = null)
        {
            var actor = actorId.HasValue ? await GetUserById(actorId.Value) : await GetCurrentUser();

            var allowed = await IsAllowed(action, relation, actor);
            if (!allowed)
            {
                throw new AccessDenied($"User is not allowed to perform action '{action}' on relation '{relation}'");
            }
        }

        public async Task EnforceRole(UserRoles role, Guid? actorId = null)
        {
            var actor = actorId.HasValue ? await GetUserById(actorId.Value) : await GetCurrentUser();

            var hasRole = await Role(role, actor);
            if (!hasRole)
            {
                throw new AccessDenied($"User does not have the required role '{role}'");
            }
        }

        public async Task EnforceRelationship(PermissionEnum action, object relation, Guid ownerId, Guid? actorId = null)
        {
            var actor = actorId.HasValue ? await GetUserById(actorId.Value) : await GetCurrentUser();

            var allowed = await Relationship(action, relation, ownerId, actor);
            if (!allowed)
            {
                throw new AccessDenied($"User is not allowed to perform action '{action}' on relation '{relation}' with ownerId '{ownerId}'");
            }
        }
    }
}
