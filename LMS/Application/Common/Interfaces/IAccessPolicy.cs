using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using LMS.Domain.User.Interfaces;

namespace LMS.Application.Common.Interfaces
{
    public interface IAccessPolicy
    {
        public Task<bool> IsAllowed<T>(IAccessUser user, string action, T relation);
        public Task<bool> Role(IAccessUser user, string role);
        public Task<bool> Relationship(IAccessUser user, string action, Guid ownerId);
        public Task<UserEntity> GetCurrentUser(); 
    }
}
