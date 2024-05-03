using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;

namespace LMS.Application.Common.Interfaces
{
    public interface IAccessPolicy
    {
        public Task<bool> CanAccess(UserRoles role, Guid? userId = null);
        public Task FailIfNoAccess(UserRoles role, Guid? userId = null);
        public Task<bool> CanAccessOrSelf(Guid byUserId, UserRoles role, Guid? userId = null);
        public Task FailIfNotSelfOrNoAccess(Guid byUserId, UserRoles role, Guid? userId = null);
        public Task<UserEntity> GetCurrentUser();
    }
}
