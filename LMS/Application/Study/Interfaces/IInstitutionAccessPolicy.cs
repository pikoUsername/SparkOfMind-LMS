using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;

namespace LMS.Application.Study.Interfaces
{
    public interface IInstitutionAccessPolicy
    {
        Task EnforceRole(InstitutionRolesEntity role, InstitutionMemberEntity member);
        Task EnforceRole(string roleName, InstitutionMemberEntity member);
        Task EnforcePermission(string permissionAcl, InstitutionMemberEntity member);
        // usage: await EnforcePermission(PermissionEnum.all, typeof(StudentEntity), member, [student.Id]);
        Task EnforcePermission(PermissionEnum action, Type subject, InstitutionMemberEntity member, Guid? subjectId = null);
        Task EnforceMembership(Guid institutionId);
        Task EnforceMembership(Guid userId, Guid institutionId); 
        Task<InstitutionMemberEntity> GetMember(Guid userId, Guid institutionId);
        Task<InstitutionMemberEntity> GetMemberByCurrentUser(Guid institutionId);
        Task<bool> IsOwner(Guid institutionId);
        Task<bool> IsOwner(Guid userId, Guid institutionId); 
    }
}
