using LMS.Application.Common.Interfaces;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure
{
    public class InstitutionAccessPolicy : IInstitutionAccessPolicy
    {
        private IApplicationDbContext _context;
        private IUser currentUser { get; }

        public InstitutionAccessPolicy(IApplicationDbContext dbContext, IUser user) {
            _context = dbContext;
            currentUser = user;
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

            Guard.Against.Null(member);

            return member; 
        }

        public async Task<InstitutionMemberEntity> GetMemberByCurrentUser(Guid institutionId)
        {
            Guard.Against.Null(currentUser.Id); 

            return await GetMember((Guid)currentUser.Id, institutionId);
        }
    }
}
