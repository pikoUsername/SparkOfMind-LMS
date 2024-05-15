using LMS.Application.Common.Interfaces;
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

        public Task<bool> IsAllowed(string action, object relation, IAccessUser? actor = null)
        {
            if (actor == null) 
                actor = (IAccessUser)GetCurrentUser(); 
            foreach (var permissionAcl in actor.GetPermissions())
            {
                if (CheckPermission(permissionAcl.Join(), action, relation))
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> Role(UserRoles role, IAccessUser? actor = null)
        {
            if (actor == null)
                actor = (IAccessUser)GetCurrentUser();
            foreach (var userRole in actor.GetRoles())
            {
                if (userRole.Role == role)
                {
                    return Task.FromResult(true); 
                }
            }
            return Task.FromResult(false);
        }

        public async Task<bool> Relationship(string action, object relation, Guid ownerId, IAccessUser? actor = null)
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

        private async Task<UserEntity> GetUserById(Guid userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);

            Guard.Against.Null(user, message: "Actor does not exists");

            return user; 
        }

        public async Task EnforceIsAllowed(string action, object relation, Guid? actorId = null)
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

        public async Task EnforceRelationship(string action, object relation, Guid ownerId, Guid? actorId = null)
        {
            var actor = actorId.HasValue ? await GetUserById(actorId.Value) : await GetCurrentUser();

            var allowed = await Relationship(action, relation, ownerId, actor);
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
                throw new Exception($"Permissions acl of the user has been compromised, parts: {parts}, acl: {permissionAcl}");
            }

            var subjectName = parts[0];
            var subjectId = parts[1];
            var subjectAction = parts[2];

            // Проверка на null и получение идентификатора сущности
            string entityId = GetEntityId(relation);

            // Проверка соответствия разрешений
            return (subjectId == entityId || subjectId == "*")
                && subjectName == relation.GetType().Name
                && subjectAction == action;
        }

        // Метод для получения идентификатора сущности
        private string GetEntityId(object relation)
        {
            if (relation == null)
            {
                throw new ArgumentNullException(nameof(relation), "Relation object cannot be null.");
            }

            if (relation is string stringRelation)
            {
                return stringRelation;
            }

            if (relation is BaseEntity entity)
            {
                return entity.Id.ToString();
            }

            throw new ArgumentException($"{nameof(relation)} entity is not convertible to BaseEntity", nameof(relation));
        }
    }
}
