using LMS.Application.Common.Interfaces;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using LMS.Domain.User.Interfaces;
using LMS.Domain.User.Services;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure
{
    public class InstitutionAccessPolicy : IInstitutionAccessPolicy
    {
        private IApplicationDbContext _context;
        private IUser currentUser { get; }
        private IAccessPolicy _accessPolicy; 

        public InstitutionAccessPolicy(
            IApplicationDbContext dbContext, 
            IUser user, 
            IAccessPolicy accessPolicy) {
            _context = dbContext;
            currentUser = user;
            _accessPolicy = accessPolicy;
        }

        public async Task EnforceRole(InstitutionRolesEntity role, InstitutionMemberEntity member)
        {

        }
        public async Task EnforceRole(string roleName, InstitutionMemberEntity member)
        {

        }
        public async Task EnforcePermission(string permissionAcl, InstitutionMemberEntity member)
        {

        }
        public async Task EnforcePermission(PermissionEnum action, Type subject, InstitutionMemberEntity member, Guid? subjectId = null)
        {
            var user = await _accessPolicy.GetCurrentUser(); 

            if (member.Roles == null)
                throw new Exception("BUG: Member roles was not loaded, not allowed to load implicitly");

            var userPermissions = user.GetPermissions();
            List<IPermissionEntity> permissions = userPermissions.ToList();
            permissions.AddRange(member.GetAllPermissions()); 

            foreach (var localPerm in permissions)
            {
                if (PermissionService.CheckPermissions(localPerm.Join(), action, subject, subjectId))
                {
                    return; 
                }
            }
            throw new AccessDenied("not enough permissions"); 
        }

        public async Task EnforceMembership(Guid institutionId)
        {
            await GetMemberByCurrentUser(institutionId); 
        }
        public async Task EnforceMembership(Guid userId, Guid institutionId)
        {
            await GetMember(userId, institutionId); 
        }

        public async Task<InstitutionMemberEntity> GetMember(Guid userId, Guid institutionId)
        {
            var member = await _context.InstitutionMembers
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.InstitutionId == institutionId);

            if (member == null)
                throw new AccessDenied("Member does not exists");

            return member; 
        }

        public async Task<InstitutionMemberEntity> GetMemberByCurrentUser(Guid institutionId)
        {
            if (currentUser.Id == null)
                throw new AccessDenied("Unauthorized");

            return await GetMember((Guid)currentUser.Id, institutionId);
        }
    }
}
